using Microsoft.AspNetCore.Components;

namespace FeatureTracker.Client.Pages.Request;

public class RequestBase : ComponentBase
{
    #region Inject
    [Inject] private NavigationManager Navigation { get; set; }
    #endregion

    #region Parameters
    [Parameter] public int Id { get; set; }
    #endregion

    #region Properties
    public bool TitleDisabled { get; set; }
    public string TitlePlaceholder { get; set; }
    //public RequestModelView requestModelView { get; set; }
    #endregion

    #region Constructor
    protected override async Task OnInitializedAsync()
    {
        TitleDisabled = true;
        //RequestModelView = new();
    }
    #endregion

    #region Methods
    protected async Task BackToRequests()
    {
        Navigation.NavigateTo("/request/dashboard");
    }

    protected async Task HandleTitleAsync()
    {
        //Will implement later the service from GeminiService to handle the title
        //This method need to call GeminiService to generate a title based on the description
        //If the service doesn't return a title, the field need to be enabled and show a placeholder
        //TitleDisabled = false;
        //TitlePlaceholder = "Enter a clear, concise title for your feature request";
    }
    #endregion
}
