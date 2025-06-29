using FeatureTracker.Application.Categories;
using FeatureTracker.Application.Companies;
using FeatureTracker.Application.Teams;
using FeatureTracker.Domain.Model.Requests;
using FeatureTracker.Domain.View.Requests;
using FeatureTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FeatureTracker.Application.Requests;

public class RequestApplication : BaseApplication
{
    #region Deendencies
    private readonly CompanyApplication _companyApplication;
    private readonly CategoryApplication _categoryApplication;
    private readonly TeamApplication _teamApplication;
    #endregion

    #region Constructor
    public RequestApplication(Context context,
                              CompanyApplication companyApplication,
                              CategoryApplication categoryApplication,
                              TeamApplication teamApplication)
        : base (context)
    {
        _companyApplication = companyApplication;
        _categoryApplication = categoryApplication;
        _teamApplication = teamApplication;
    }
    #endregion

    #region Methods
    public async Task<Request> GetRequestByIdAsync(int requestId) =>
        await _context.Request
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RequestId == requestId);

    public async Task<RequestView> GetRequestAsync(int requestId)
    {
        if (requestId == 0)
            throw new ArgumentException("Request ID cannot be null or empty.", nameof(requestId));

        var request = await GetRequestByIdAsync(requestId);

        if (request is null)
            throw new KeyNotFoundException($"Request with ID {requestId} not found.");

        var company = await _companyApplication.GetCompanyByIdAsync(request.CompanyId);

        var category = await _categoryApplication.GetCategoryByIdAsync(request.CategoryId);

        var team = await _teamApplication.GetTeamByIdAsync(request.TeamId);

        return new RequestView();
    }
    #endregion
}
