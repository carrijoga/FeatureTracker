using FeatureTracker.Application.Companies;
using FeatureTracker.Application.Parameters.Email;
using FeatureTracker.Application.Services.LoginAttempt;
using FeatureTracker.Application.Users;
using FeatureTracker.Domain.Model.Companies;
using FeatureTracker.Domain.Model.Users;
using FeatureTracker.Domain.Security;
using FeatureTracker.Domain.View.Email.Users;
using FeatureTracker.Infrastructure;
using FeatureTracker.Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Authentication;
using ApplicationException = FeatureTracker.Shared.Security.ApplicationException;

namespace FeatureTracker.Application.Authentication;

public class AuthApplication : BaseApplication
{
    #region Dependencies
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILoginAttemptService _loginAttemptService;
    private readonly CompanyApplication _companyApplication;
    private readonly UserApplication _userApplication;
    private readonly EmailSenderApplication _emailSenderApplication;
    #endregion

    #region Constructor

    public AuthApplication(Context context,
                           IConfiguration configuration,
                           IPasswordHasher passwordHasher,
                           ILoginAttemptService loginAttemptService,
                           CompanyApplication companyApplication,
                           UserApplication userApplication,
                           EmailSenderApplication emailSenderApplication
    ) : base(context)
    {
        _configuration = configuration;
        _passwordHasher = passwordHasher;
        _loginAttemptService = loginAttemptService;
        _companyApplication = companyApplication;
        _userApplication = userApplication;
        _emailSenderApplication = emailSenderApplication;
    }
    #endregion

    #region Methods
    public async Task<bool> PreRegisterUserAsync(string email, string creatorId)
    {
        var (exists, message) = await _userApplication.VerifyEmailInvitationAsync(email);

        if (!exists)
        {
            var InviteToken = await _userApplication.GenerateUserInvitation(email, creatorId);
            var subject = "Convite para criar sua conta no FeatureTracker";

            await _emailSenderApplication.SendEmail(email,
                                                    subject,
                                                    new UserInvite().GetMessageEmail(InviteToken.Item2, InviteToken.Item1));

            return true;
        }
        else
            throw new AuthenticationException(message);
    }

    public async Task<UserLoginViewModel> LoginAsync(string? email, string? username, string password)
    {
        try
        {
            var userKey = (email ?? username ?? string.Empty).ToLower();

            if (string.IsNullOrWhiteSpace(userKey))
                throw new ApplicationException("Email or username is required, try again!", StatusCodes.Status400BadRequest);

            if (await _loginAttemptService.IsLockedOutAsync(userKey, 5))
                throw new ApplicationException("Too many failed login attempts. Please try again later.", StatusCodes.Status429TooManyRequests);

            var user = await _userApplication.GetUserByEmailOrUserNameAsync(userKey);

            if (user is null)
            {
                await _loginAttemptService.IncrementAsync(userKey);
                throw new ApplicationException("User not found, try again!", StatusCodes.Status404NotFound);
            }

            if (!user.VerifyPassword(password, _passwordHasher))
            {
                await _loginAttemptService.IncrementAsync(userKey);
                throw new ApplicationException("Invalid password, try again!", StatusCodes.Status401Unauthorized);
            }

            await _loginAttemptService.ResetAsync(userKey);
            var expires = DateTime.UtcNow.AddDays(1).Date;

            return new UserLoginViewModel
            {
                Token = new TokenAuthApplication(_configuration).GenerateToken(user, expires),
                RefreshToken = null, //Future implementation
                Expires = expires,
                Message = "Login successful"
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred! {ex.Message}", StatusCodes.Status400BadRequest);
        }
    }

    public async Task<bool> RegisterAsync(UserRegisterViewModel userRegisterInfo)
    {
        ArgumentNullException.ThrowIfNull(userRegisterInfo);

        await userRegisterInfo.IsValid();

        var userKey = (userRegisterInfo.Email ?? userRegisterInfo.Username ?? string.Empty).ToLower();

        if (await _userApplication.VerifyExistingUserByEmailOrUsername(userKey))
            throw new AuthenticationException("User already exists");

        _context.Users.Add(new User().CreateNewUser(userRegisterInfo, new Company(), _passwordHasher));

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RegisterNewAsync(UserRegisterViewModel userRegisterInfo)
    {
        ArgumentNullException.ThrowIfNull(userRegisterInfo);

        await userRegisterInfo.IsValid();

        var userKey = (userRegisterInfo.Email ?? userRegisterInfo.Username ?? string.Empty).ToLower();

        if (await _userApplication.VerifyExistingUserByEmailOrUsername(userKey))
            throw new AuthenticationException("User already exists");

        Guid.TryParse(userRegisterInfo.TokenCompany, out Guid creatorGuid);

        if (await _companyApplication.CheckCompanyTokenAsync(creatorGuid))
        {
            var company = await _companyApplication.ValidCompanyByTokenAsync(creatorGuid);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Users.Add(new User().CreateNewUser(userRegisterInfo, company, _passwordHasher));

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }

            return true;
        }

        return false;
    }

    public async Task<UserRegisterViewModel> ValidateTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(token))
            throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");

        if (!Guid.TryParse(token, out Guid tokenGuid))
            throw new ArgumentException("Invalid token format. It must be a valid GUID.", nameof(token));

        var userInvite = await _context.UserInvite
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Token == tokenGuid && !x.IsUsed && x.ExpirationDate > DateTime.Now);

        if (userInvite is null)
            throw new AuthenticationException("Invalid or expired invitation token.");

        return new UserRegisterViewModel
        {
            Email = userInvite.Email,
            TokenCompany = userInvite.Token.ToString()
        };
    }

    public async Task<bool> RecoverPasswordAsync(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user is null)
            throw new AuthenticationException("User not found, try again!");

        var recoveryPasswordEmail = new RecoveryPasswordEmail(user);

        if (await new EmailSenderApplication(_configuration)
                .SendEmail(email,
                    recoveryPasswordEmail.Subject,
                    recoveryPasswordEmail.GetMessageEmail(user)))
            return true;

        return true;
    }

    public async Task<bool> CheckUsername(string username) =>
        await _context.Users.AnyAsync(x => x.Username == username);

    #endregion
}
