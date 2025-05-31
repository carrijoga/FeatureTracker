using FeatureTracker.Client.Services.Authentication;
using Microsoft.AspNetCore.Components;

namespace FeatureTracker.Client.Pages.Authentication;

public class LogoutBase : ComponentBase
{
    [Inject] private NavigationManager Navigation { get; set; }

    [Inject] private AuthenticationService authenticationService { get; set; }

    protected async override Task OnInitializedAsync()
    {
        await LogoutRedirect();
    }
    protected async Task LogoutRedirect()
    {
        await authenticationService.Logout();
        Navigation.NavigateTo("/login");
    }
}
