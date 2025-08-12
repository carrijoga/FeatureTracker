using FeatureTracker.Client;
using FeatureTracker.Client.Services.Authentication;
using FeatureTracker.Shared.System;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Extensions;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
    Timeout = TimeSpan.FromSeconds(30)
});

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<ClientParameters>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>(
    provider => provider.GetRequiredService<AuthenticationService>());

builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationService>(
    provider => provider.GetRequiredService<AuthenticationService>());

//builder.Services.AddAuthenticationServices();
builder.Services.AddMudServices();
builder.Services.AddMudServicesWithExtensions();
builder.Services.AddMudExtensions();

await builder.Build().RunAsync();
