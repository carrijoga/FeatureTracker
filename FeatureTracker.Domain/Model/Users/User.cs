using FeatureTracker.Shared.ViewModel;
using FeatureTracker.Domain.Model.Persons;
using FeatureTracker.Domain.Security;
using FeatureTracker.Domain.Model.Companies;
using FeatureTracker.Domain.Model.Profiles;

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
    public int CompanyId { get; set; }
    //public int ProfileId { get; set; }

    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public int PasswordIterations { get; set; }
    public bool IsActive { get; set; }
    //public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    //public Profile Profile { get; set; }
    public Person? Person { get; set; }
    public Company Company { get; set; }

    #endregion

    #region Methods

    public User CreateNewUser(UserRegisterViewModel userRegisterInfo, Company company, IPasswordHasher passwordHasher)
    {
        var (hash, salt, iterations) = passwordHasher.HashPassword(userRegisterInfo.Password);

        return new User
        {
            CompanyId = 2,
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

    public string GetFullName() =>
        $"{Person?.GetFullName()}";

    #endregion
}
