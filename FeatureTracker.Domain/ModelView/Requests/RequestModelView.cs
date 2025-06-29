using FeatureTracker.Domain.Enums.Requests;

namespace FeatureTracker.Domain.ModelView.Requests;

public class RequestModelView
{
    #region Constructor
    public RequestModelView() { }
    #endregion

    #region Properties
    public int RequestId { get; set; }
    public int RequestIdShow { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public RequestStatus Status { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public RequestPriority Priority { get; set; }
    public RequestImpact Impact { get; set; }
    public int AssignedTo { get; set; }
    public string AssignedName { get; set; }
    public DateTime? EstimatedAt { get; set; }
    public DateTime? DeliveryAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? UpdatedBy { get; set; }
    #endregion

    #region Methods
    #endregion
}
