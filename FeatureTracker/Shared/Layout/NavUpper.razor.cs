using Microsoft.AspNetCore.Components;

namespace FeatureTracker.Client.Shared.Layout;

public class NavUpperBase : ComponentBase
{
    #region Inject

    [Inject] protected NavigationManager Navigation { get; set; }

    #endregion

    #region Parameter

    [Parameter] public EventCallback OnToggleDarkMode { get; set; }

    #endregion

    #region Methods

    protected async Task ToggleDarkMode()
    {
        if (OnToggleDarkMode.HasDelegate)
            await OnToggleDarkMode.InvokeAsync(null);
    }

    protected void RedirectRoute(string route) =>
        Navigation.NavigateTo(route);

    #endregion
}
