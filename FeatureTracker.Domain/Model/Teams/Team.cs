namespace FeatureTracker.Domain.Model.Teams;

public class Team
{
    #region Constructor
    public Team() { }
    #endregion

    #region Properties
    public int TeamId { get; set; }
    public string TeamName { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public bool IsActive { get; set; }
    #endregion
}
