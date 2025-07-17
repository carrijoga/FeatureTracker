namespace FeatureTracker.Shared.System;

public class ClientParameters
{
    #region Constructor

    public ClientParameters() { }
    #endregion

    #region Properties

    public string PersonName { get; set; }
    public string ProfileName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool ThemePreference { get; set; }

    #endregion

    #region Change Notification
    public event Action? OnChanged;

    public void SetAll(ClientParameters other)
    {
        PersonName = other.PersonName;
        ProfileName = other.ProfileName;
        Username = other.Username;
        Email = other.Email;
        ThemePreference = other.ThemePreference;
        OnChanged?.Invoke();
    }
    #endregion
}
