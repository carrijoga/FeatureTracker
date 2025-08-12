using FeatureTracker.Client.Services.Authentication;

namespace FeatureTracker.Client.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
