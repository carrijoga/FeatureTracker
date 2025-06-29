using FeatureTracker.Domain.Model.Teams;
using FeatureTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FeatureTracker.Application.Teams;

public class TeamApplication : BaseApplication
{
    public TeamApplication(Context context) : base(context) { }

    public async Task<Team> GetTeamByIdAsync(int teamId) =>
        await _context.Team
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TeamId == teamId);

}
