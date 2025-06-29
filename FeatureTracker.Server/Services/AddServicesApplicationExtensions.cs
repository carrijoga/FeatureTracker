using FeatureTracker.Application.Authentication;
using FeatureTracker.Application.Categories;
using FeatureTracker.Application.Companies;
using FeatureTracker.Application.Parameters.Email;
using FeatureTracker.Application.Requests;
using FeatureTracker.Application.Teams;
using FeatureTracker.Application.Users;

namespace FeatureTracker.Server.Services;

public static class AddServicesApplicationExtensions
{
    public static IServiceCollection AddServicesApplication(this IServiceCollection services)
    {
        services.AddScoped<AuthApplication>();
        services.AddScoped<TokenAuthApplication>();
        services.AddScoped<EmailSenderApplication>();

        services.AddScoped<UserApplication>();
        services.AddScoped<TeamApplication>();
        services.AddScoped<CompanyApplication>();
        services.AddScoped<CategoryApplication>();
        services.AddScoped<RequestApplication>();

        return services;
    }
}
