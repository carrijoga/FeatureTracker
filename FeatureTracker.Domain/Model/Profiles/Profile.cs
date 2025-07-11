namespace FeatureTracker.Domain.Model.Profiles;

public class Profile
{
    #region Constructor
    public Profile() { }
    #endregion

    #region Properties
    public int ProfileId { get; set; }
    public string ProfileName { get; set; }
    public string Description { get; set; }


    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int UpdatedBy { get; set; }
    #endregion
}
