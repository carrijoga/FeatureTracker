using FeatureTracker.Shared.System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeatureTracker.Client.Shared.Layout;

public class UserMenuBase : ComponentBase, IDisposable
{
    #region Inject

    [Inject] protected NavigationManager Navigation { get; set; }
    [Inject] protected ClientParameters clientParameters { get; set; }

    #endregion

    #region Parameter

    [Parameter] public EventCallback OnToggleDarkMode { get; set; }

    #endregion

    #region Properties

    protected bool isDarkMode { get; set; }
    protected string DarkModeIcon = Icons.Material.Outlined.DarkMode;
    protected string DarkModeTooltip = "Dark Mode";

    #endregion

    #region Methods
    protected override void OnInitialized()
    {
        clientParameters.OnChanged += OnClientParametersChanged;
    }

    private void OnClientParametersChanged()
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        clientParameters.OnChanged -= OnClientParametersChanged;
    }

    protected async Task ToggleDarkMode()
    {
        var isDarkMode = DarkModeIcon == Icons.Material.Outlined.DarkMode;
        DarkModeIcon = isDarkMode ? Icons.Material.Outlined.LightMode : Icons.Material.Outlined.DarkMode;
        DarkModeTooltip = isDarkMode ? "Light Mode" : "Dark Mode";

        if (OnToggleDarkMode.HasDelegate)
            await OnToggleDarkMode.InvokeAsync(null);
    }

    protected void Logout() =>
        Navigation.NavigateTo("/logout");

    protected void RedirectRoute(string route) =>
        Navigation.NavigateTo(route);

    #endregion
}
