using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeatureTracker.Client.Shared.Layout.Dashboard;

public class DashCardChartBase : ComponentBase
{
    #region Parameters
    [Parameter] public string Title { get; set; }
    [Parameter] public string Description { get; set; }
    [Parameter] public ChartType ChartType { get; set; }
    [Parameter] public string[] InputData { get; set; }
    [Parameter] public string[] InputLabel { get; set; }
    #endregion
}
