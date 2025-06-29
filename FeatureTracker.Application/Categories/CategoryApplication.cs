using FeatureTracker.Domain.Model.Categories;
using FeatureTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FeatureTracker.Application.Categories;

public class CategoryApplication : BaseApplication
{
    #region Constructor
    public CategoryApplication(Context context) : base(context) { }
    #endregion

    #region Methods
    public async Task<Category> GetCategoryByIdAsync(int categoryId) =>
        await _context.Category
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CategoryId == categoryId);
    #endregion
}
