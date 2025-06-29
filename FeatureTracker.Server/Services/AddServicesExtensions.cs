using FeatureTracker.Application.Authentication;
using FeatureTracker.Application.Services.LoginAttempt;
using FeatureTracker.Domain.Security;
using FeatureTracker.Infrastructure;
using FeatureTracker.Server.Services.Gemini;

namespace FeatureTracker.Server.Services;

public static class AddServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<Context>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ILoginAttemptService, LoginAttemptService>();

        services.AddScoped<GeminiService>();

        return services;
    }
}
