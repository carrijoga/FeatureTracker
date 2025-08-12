using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace FeatureTracker.Client.Pages.Request;

public class RequestDashboardBase : ComponentBase
{
    #region Inject
    [Inject] private NavigationManager Navigation { get; set; }
    #endregion

    #region Properties
    public string SearchText { get; set; }
    public bool LoadingActive { get; set; }
    public struct RequestDashboardModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Status { get; set; }
        public Color StatusColor { get; set; }
        public string Company { get; set; }
        public DateTime Submitted { get; set; }
        public DateTime EstimatedDelivery { get; set; }
        public int NumberComments { get; set; }
    }

    public List<RequestDashboardModel> RequestDashboard { get; set; } = [];
    #endregion

    #region Constructor
    protected override async Task OnInitializedAsync()
    {
        await SearchForRequests();
    }

    private async Task SearchForRequests()
    {
        LoadingActive = true;
        await Task.Delay(1000);

        RequestDashboard = new List<RequestDashboardModel>
        {
            new()
            {
                Id = "GR-2023-001",
                Title = "Enhanced Reporting Dashboard",
                ShortDescription = "Add more visualization options and export capabilities.",
                Status = "In Development",
                StatusColor = Color.Info,
                Company = "Company X",
                Submitted = DateTime.Now.AddDays(-10),
                EstimatedDelivery = DateTime.Now.AddYears(1),
                NumberComments = 8
            },
            new()
            {
                Id = "GR-2023-002",
                Title = "Bulk User Import",
                ShortDescription = "Allow administrators to import users in bulk via CSV.",
                Status = "Completed",
                StatusColor = Color.Success,
                Company = "Company Y",
                Submitted = DateTime.Now.AddDays(-20),
                EstimatedDelivery = DateTime.Now.AddDays(-5),
                NumberComments = 0,
            },
            new()
            {
                Id = "GR-2023-003",
                Title = "Mobile App Notifications",
                ShortDescription = "Implement push notifications for the mobile app.",
                Status = "Under Review",
                StatusColor = Color.Warning,
                Company = "Company O",
                Submitted = DateTime.Now.AddDays(-20),
                EstimatedDelivery = DateTime.Now.AddDays(-5),
                NumberComments = 3,
            }
        };

        LoadingActive = false;
        StateHasChanged();
    }
    #endregion

    #region Methods
    protected async Task CreateNewRequest()
    {
        Navigation.NavigateTo("/request");
    }
    #endregion
}
