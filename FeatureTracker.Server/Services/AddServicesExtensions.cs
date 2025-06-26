using FeatureTracker.Application.Authentication;
using FeatureTracker.Application.Services.LoginAttempt;
using FeatureTracker.Domain.Security;

namespace FeatureTracker.Server.Services;

public static class AddServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ILoginAttemptService, LoginAttemptService>();

        services.AddScoped<AuthApplication>();
        services.AddScoped<TokenAuthApplication>();

        return services;
    }
}
