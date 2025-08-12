using Microsoft.AspNetCore.Components;
using FeatureTracker.Client.Models;
using FeatureTracker.Client.Services;

namespace FeatureTracker.Client.Shared.Layout;

public class NotificationComponentBase : ComponentBase, IDisposable
{
    [Inject] protected INotificationService NotificationService { get; set; } = default!;

    protected bool Open;
    protected List<NotificationModel> Notifications { get; set; } = [];
    protected int UnreadCount { get; set; }

    protected override void OnInitialized()
    {
        NotificationService.OnNotificationsChanged += OnNotificationsChanged;
        LoadNotifications();
    }

    protected void ToggleOpen()
    {
        Open = !Open;
    }

    protected void MarkAsRead(Guid notificationId)
    {
        NotificationService.MarkAsRead(notificationId);
    }

    protected void MarkAllAsRead()
    {
        NotificationService.MarkAllAsRead();
    }

    protected void ViewAllNotifications()
    {
        // TODO: Navigate to a full notifications page
        // For now, just close the popover
        Open = false;
    }

    protected Dictionary<string, List<NotificationModel>> GetGroupedNotifications()
    {
        return Notifications
            .GroupBy(n => n.Category)
            .OrderBy(g => GetCategoryOrder(g.Key))
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    private void LoadNotifications()
    {
        Notifications = NotificationService.GetNotifications();
        UnreadCount = NotificationService.GetUnreadCount();
    }

    private void OnNotificationsChanged()
    {
        LoadNotifications();
        InvokeAsync(StateHasChanged);
    }

    private int GetCategoryOrder(string category)
    {
        return category switch
        {
            "Today" => 1,
            "Yesterday" => 2,
            _ when category.Contains("day") => 3,
            _ => 4
        };
    }

    public void Dispose()
    {
        NotificationService.OnNotificationsChanged -= OnNotificationsChanged;
    }
}

// Remove the old TwoStringItems record as it's no longer needed
