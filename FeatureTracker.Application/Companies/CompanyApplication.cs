using FeatureTracker.Domain.Model.Companies;
using FeatureTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FeatureTracker.Application.Companies;

public class CompanyApplication : BaseApplication
{
    #region Constructor
    public CompanyApplication(Context context) : base(context) { }
    #endregion

    #region Methods
    public async Task<Company> GetCompanyByIdAsync(int companyId) =>
        await _context.Company
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CompanyId == companyId);

    public async Task GenerateCompanyTokenAsync() => throw new NotImplementedException();

    public async Task<bool> CheckCompanyTokenAsync(Guid companyToken) => throw new NotImplementedException();

    public async Task<Company> ValidCompanyByTokenAsync(Guid companyToken)
    {

        return new Company();
    }

    #endregion
}
