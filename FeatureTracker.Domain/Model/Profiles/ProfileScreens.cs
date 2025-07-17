using FeatureTracker.Domain.Enums.Profile;

namespace FeatureTracker.Domain.Model.Profiles;

public class ProfileScreens
{
    public int ProfileScreensId { get; set; }
    public int ProfileId { get; set; }
    public Screens Screen { get; set; }
    public bool CanCreate { get; set; }
    public bool CanRead { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanDelete { get; set; }

    public Profile Profile { get; set; }
}
