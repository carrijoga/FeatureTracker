using FeatureTracker.Shared.ViewModel;
using FeatureTracker.Domain.Model.Persons;
using FeatureTracker.Domain.Security;

namespace FeatureTracker.Domain.Model.Users;

public class User
{
    #region Constructor
    public User()
    {
        Id = Guid.NewGuid();
        IsActive = true;
        CreatedAt = DateTime.Now;
    }
    #endregion

    #region Proprieties

    public Guid Id { get; set; }
    public int UserId { get; set; }
    public int PersonId { get; set; }

    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public int PasswordIterations { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Person? Person { get; set; }

    #endregion

    #region Methods

    public User CreateNewUser(UserRegisterViewModel userRegisterInfo, IPasswordHasher passwordHasher)
    {
        var (hash, salt, iterations) = passwordHasher.HashPassword(userRegisterInfo.Password);

        return new User
        {
            Email = userRegisterInfo.Email,
            PasswordHash = hash,
            PasswordSalt = salt,
            PasswordIterations = iterations,
            Username = userRegisterInfo.Username,
            Person = new Person().CreateNewPerson(userRegisterInfo)
        };
    }

    public bool VerifyPassword(string password, IPasswordHasher passwordHasher) =>
        passwordHasher.Verify(
            password,
            PasswordHash,
            PasswordSalt,
            PasswordIterations);

    #endregion
}
