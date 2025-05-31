namespace FeatureTracker.Shared.Account;

public class UserAuthInfo
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime Expires { get; set; }
    public string? Message { get; set; }
}
