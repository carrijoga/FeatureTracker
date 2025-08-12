using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeatureTracker.Client.Shared.Layout.Dashboard;

public class DashCardBase : ComponentBase
{
    #region Parameters
    [Parameter] public string Title { get; set; }
    [Parameter] public string Description { get; set; }
    [Parameter] public string SubDescription { get; set; }
    [Parameter] public string PrincipalIcon { get; set; }

    [Parameter] public string SecondaryDescription { get; set; }
    [Parameter] public string SecondarySubDescription { get; set; }
    [Parameter] public string SecondaryIcon { get; set; }
    [Parameter] public Color SecondaryColor { get; set; }
    #endregion
}
