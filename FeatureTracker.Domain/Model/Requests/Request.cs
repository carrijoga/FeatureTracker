using FeatureTracker.Domain.Enums.Requests;
using FeatureTracker.Domain.Model.Categories;
using FeatureTracker.Domain.Model.Companies;
using FeatureTracker.Domain.Model.Teams;
using FeatureTracker.Domain.View.Requests;

namespace FeatureTracker.Domain.Model.Requests;

public class Request
{
    #region Constructor
    public Request() { }
    #endregion

    #region Proprieties
    public int RequestId { get; set; }
    public int CompanyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TicketId { get; set; } // Optional, can be used for integration with FreshDesk API or similar systems
    public RequestStatus Status { get; set; }
    public int CategoryId { get; set; }
    public RequestPriority Priority { get; set; }
    public RequestImpact Impact { get; set; }
    public int TeamId { get; set; }
    public DateTime? EstimatedAt { get; set; }
    public DateTime? DeliveryAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? UpdatedBy { get; set; }

    public Category Category { get; set; }
    public Team Team { get; set; }
    #endregion

    #region Methods
    #endregion
}
