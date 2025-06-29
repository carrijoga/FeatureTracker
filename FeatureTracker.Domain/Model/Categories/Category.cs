namespace FeatureTracker.Domain.Model.Categories;

public class Category
{
    #region Constructor
    public Category() { }
    #endregion

    #region Properties
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryEmoji { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public bool IsActive { get; set; }
    #endregion
}
