namespace FeatureTracker.Shared.ViewModel;

public class UserRegisterViewModel
{
    #region Properties

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDay { get; set; }
    public string? Cpf { get; set; }
    public string? Cnpj { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public bool AcceptTerms { get; set; }
    public string TokenCompany { get; set; }

    #endregion

    #region Methods

    public async Task IsValid()
    {
        if (!AcceptTerms)
            throw new InvalidOperationException("Accept terms is required");

        if (string.IsNullOrWhiteSpace(FirstName)
            || string.IsNullOrWhiteSpace(LastName))
            throw new InvalidOperationException("First name and Last name are required");

        if (string.IsNullOrWhiteSpace(Username))
            throw new InvalidOperationException("Username is required");

        if (string.IsNullOrWhiteSpace(Email))
            throw new InvalidOperationException("Email is required");

        if (await VerifyPassword(Password))
            throw new InvalidOperationException("Password does not follow standards");

        if (string.IsNullOrWhiteSpace(Password))
            throw new InvalidOperationException("Password is required");

        if (Password != ConfirmPassword)
            throw new InvalidOperationException("Password and Confirm password are different");
    }

    private async Task<bool> VerifyPassword(string password) =>
        // if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        //     return true;
        //
        // bool hasUpperCase = password.Any(char.IsUpper);
        // bool hasLowerCase = password.Any(char.IsLower);
        // bool hasNumber = password.Any(char.IsDigit);
        // bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));
        //
        // return !hasUpperCase || !hasLowerCase || !hasNumber || !hasSpecialChar;
        false;

    #endregion
}
