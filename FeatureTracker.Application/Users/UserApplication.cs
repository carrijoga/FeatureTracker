using FeatureTracker.Domain.Enums.Users;
using FeatureTracker.Domain.Model.Users;
using FeatureTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FeatureTracker.Application.Users;

public class UserApplication : BaseApplication
{
    #region Constructor
    public UserApplication(Context context) : base(context) { }
    #endregion

    #region Methods
    public async Task<User> GetUserByEmailOrUserNameAsync(string userEmailOrUsername) =>
        await _context.Users
            .Include(x => x.Person)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == userEmailOrUsername || x.Username == userEmailOrUsername);

    public async Task<User> GetUserByIdAsync(Guid userId) =>
        await _context.Users
                .Include(x => x.Person)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

    public async Task<bool> VerifyExistingUserByEmailOrUsername(string userKey) =>
        await _context.Users
            .AsNoTracking()
            .AnyAsync(x => x.Email == userKey || x.Username == userKey);

    public async Task<(bool, string)> VerifyEmailInvitationAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");

        if (await VerifyExistingUserByEmailOrUsername(email))
            return (true, "Existing user with this email.");

        if (await _context.UserInvite
            .AsNoTracking()
            .AnyAsync(x => x.Email == email && !x.IsUsed && x.ExpirationDate > DateTime.Now))
            return (true, "Valid invitation found for this email.");

        return (false, "No valid invitation found for this email.");
    }

    public async Task<(string, string)> GenerateUserInvitation(string email, string creatorId)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");

        if (!Guid.TryParse(creatorId, out Guid creatorGuid))
            throw new ArgumentException("Invalid creatorId format. It must be a valid GUID.", nameof(creatorId));

        var userInviter = await GetUserByIdAsync(creatorGuid);

        UserInvite userInvite = new()
        {
            Email = email,
            Token = Guid.NewGuid(),
            ExpirationDate = DateTime.Now.AddDays(7),
            CreatedAt = DateTime.Now,
            CreatedBy = userInviter.UserId,
            IsUsed = false,
            InviteStatus = InviteStatus.Pending,
        };

        await _context.UserInvite.AddAsync(userInvite);
        await _context.SaveChangesAsync();

        return (userInviter.GetFullName(), userInvite.Token.ToString());
    }
    #endregion
}
