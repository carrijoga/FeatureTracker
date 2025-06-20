using Microsoft.AspNetCore.Components;

namespace FeatureTracker.Client.Shared.Layout;

public class NavUpperBase : ComponentBase
{
    #region Injects

    [Inject] protected NavigationManager Navigation { get; set; }

    #endregion

    #region Parameters

    [Parameter] public EventCallback OnToggleDarkMode { get; set; }
    [Parameter] public EventCallback OnToggleMenuBar { get; set; }

    #endregion

    #region Properties
    protected bool _isOpen { get; set; }
    #endregion

    #region Methods

    protected async Task ToggleDarkMode()
    {
        if (OnToggleDarkMode.HasDelegate)
            await OnToggleDarkMode.InvokeAsync(null);
    }

    protected async Task ToggleMenuBar()
    {
        if (OnToggleMenuBar.HasDelegate)
            await OnToggleMenuBar.InvokeAsync(null);
    }

    protected void RedirectRoute(string route) =>
        Navigation.NavigateTo(route);

    #endregion
}
