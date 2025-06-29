
namespace FeatureTracker.Domain.Model.Companies;

public class Company
{
    #region Properties
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CPFCNPJ { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public bool IsActive { get; set; }
    #endregion

    #region Methods
    #endregion
}
