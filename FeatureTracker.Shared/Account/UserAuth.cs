namespace FeatureTracker.Shared.Account;

public class UserAuth
{
    public string? Email { get; set; } = string.Empty;
    public string? Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = false;
}
