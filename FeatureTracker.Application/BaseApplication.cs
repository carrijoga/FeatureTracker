
using FeatureTracker.Infrastructure;

namespace FeatureTracker.Application;

public class BaseApplication
{
    protected readonly Context _context;

    public BaseApplication(Context context)
    {
        _context = context;
    }
}
