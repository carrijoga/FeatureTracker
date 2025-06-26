using System.Security.Authentication;
using FeatureTracker.Domain.Model.Users;
using FeatureTracker.Domain.Security;
using FeatureTracker.Domain.View.Email.Users;
using FeatureTracker.Infrastructure;
using FeatureTracker.Shared.ViewModel;
using FeatureTracker.Application.Parameters.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using ApplicationException = FeatureTracker.Shared.Security.ApplicationException;
using FeatureTracker.Application.Services.LoginAttempt;

namespace FeatureTracker.Application.Authentication;

public class AuthApplication
{
    #region Dependencies
    private readonly Context _context;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILoginAttemptService _loginAttemptService;
    #endregion

    #region Constructor

    public AuthApplication(Context context,
                           IConfiguration configuration,
                           IPasswordHasher passwordHasher,
                           ILoginAttemptService loginAttemptService)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
        _loginAttemptService = loginAttemptService;
    }

    #endregion

    #region Methods

    public async Task<UserLoginViewModel> LoginAsync(string? email, string? username, string password)
    {
        var userKey = (email ?? username ?? string.Empty).ToLower();

        if (string.IsNullOrWhiteSpace(userKey))
            throw new ApplicationException("Email or username is required, try again!", StatusCodes.Status400BadRequest);

        if (await _loginAttemptService.IsLockedOutAsync(userKey, 5))
            throw new ApplicationException("Too many failed login attempts. Please try again later.", StatusCodes.Status429TooManyRequests);

        var user = await _context.Users
            .Include(x => x.Person)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email || x.Username == username);

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

    public async Task<bool> RegisterAsync(UserRegisterViewModel userRegisterInfo)
    {
        ArgumentNullException.ThrowIfNull(userRegisterInfo);

        await userRegisterInfo.IsValid();

        if (await _context.Users.AnyAsync(x => x.Email == userRegisterInfo.Email
                                                 || x.Username == userRegisterInfo.Username))
            throw new AuthenticationException("User already exists");

        _context.Users.Add(new User().CreateNewUser(userRegisterInfo, _passwordHasher));
        await _context.SaveChangesAsync();

        return true;
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
