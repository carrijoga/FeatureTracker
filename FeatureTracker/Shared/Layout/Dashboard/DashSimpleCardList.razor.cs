using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace FeatureTracker.Client.Shared.Layout.Dashboard;

public class DashSimpleCardListBase : ComponentBase
{
    #region Inject
    [Inject] public IJSRuntime js { get; set; }
    #endregion

    #region Parameters
    [Parameter] public string Title { get; set; }
    [Parameter] public string IconTitle { get; set; }
    [Parameter] public string Status { get; set; }
    [Parameter] public string DeliveryDate { get; set; }
    #endregion

    #region Methods
    protected Color GetColorStatus()
    {
        return Status switch
        {
            "In Development" => Color.Info,
            "Under Review" => Color.Warning,
            "Completed" => Color.Success,
            "In Testing" => Color.Surface,
            "Planned" => Color.Warning,
            _ => Color.Default
        };
    }
    #endregion
}
