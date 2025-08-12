using FeatureTracker.Client.Models;

namespace FeatureTracker.Client.Services;

public interface INotificationService
{
    event Action? OnNotificationsChanged;
    List<NotificationModel> GetNotifications();
    void MarkAsRead(Guid notificationId);
    void MarkAllAsRead();
    int GetUnreadCount();
    void AddNotification(NotificationModel notification);
}

public class NotificationService : INotificationService
{
    private readonly List<NotificationModel> _notifications = [];

    public event Action? OnNotificationsChanged;

    public NotificationService()
    {
        // Add some sample notifications
        _notifications.AddRange(new List<NotificationModel>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Maintenance request update",
                Message = "The maintenance request for John Doe in Apartment 301 has been Completed. The issue was a leaking faucet in the kitchen.",
                Category = "Today",
                CreatedAt = DateTime.Now.AddHours(-5),
                Icon = "🏠"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Rent Payment Confirmation",
                Message = "We have received the rent payment of $1,200 for Jane Smith in Apartment 102. The payment was processed successfully.",
                Category = "Today",
                CreatedAt = DateTime.Now.AddHours(-7),
                Icon = "💰"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Lease Renewal Reminder",
                Message = "The lease for Esther Howard in Apartment 308 is set to expire on October 15, 2023. Please take appropriate action to initiate lease renewal discussions.",
                Category = "Today",
                CreatedAt = DateTime.Now.AddHours(-7),
                Icon = "📋"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "New Feature Request",
                Message = "A new feature request has been submitted for the mobile app notifications system.",
                Category = "Yesterday",
                CreatedAt = DateTime.Now.AddDays(-1),
                Icon = "🔔"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "System Update Complete",
                Message = "The scheduled system maintenance has been completed successfully.",
                Category = "Yesterday",
                CreatedAt = DateTime.Now.AddDays(-1),
                Icon = "⚙️"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Payment Processed",
                Message = "Monthly subscription payment has been processed successfully.",
                Category = "2 days ago",
                CreatedAt = DateTime.Now.AddDays(-2),
                Icon = "💳"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "User Registration",
                Message = "New user has registered for the platform.",
                Category = "3 days ago",
                CreatedAt = DateTime.Now.AddDays(-3),
                Icon = "👤"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Security Alert",
                Message = "Unusual login activity detected on your account.",
                Category = "3 days ago",
                CreatedAt = DateTime.Now.AddDays(-3),
                Icon = "🔒"
            }
        });
    }

    public List<NotificationModel> GetNotifications()
    {
        return _notifications.OrderByDescending(n => n.CreatedAt).ToList();
    }

    public void MarkAsRead(Guid notificationId)
    {
        var notification = _notifications.FirstOrDefault(n => n.Id == notificationId);
        if (notification != null && !notification.IsRead)
        {
            notification.IsRead = true;
            OnNotificationsChanged?.Invoke();
        }
    }

    public void MarkAllAsRead()
    {
        var hasChanges = false;
        foreach (var notification in _notifications.Where(n => !n.IsRead))
        {
            notification.IsRead = true;
            hasChanges = true;
        }

        if (hasChanges)
        {
            OnNotificationsChanged?.Invoke();
        }
    }

    public int GetUnreadCount()
    {
        return _notifications.Count(n => !n.IsRead);
    }

    public void AddNotification(NotificationModel notification)
    {
        _notifications.Insert(0, notification);
        OnNotificationsChanged?.Invoke();
    }
}
