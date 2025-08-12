namespace FeatureTracker.Client.Models;

public class NotificationModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsRead { get; set; } = false;
    public string? Icon { get; set; }
    public string TimeAgo => GetTimeAgo();

    private string GetTimeAgo()
    {
        var timeSpan = DateTime.Now - CreatedAt;
        
        if (timeSpan.TotalDays >= 1)
            return $"{(int)timeSpan.TotalDays}d ago";
        if (timeSpan.TotalHours >= 1)
            return $"{(int)timeSpan.TotalHours}h ago";
        if (timeSpan.TotalMinutes >= 1)
            return $"{(int)timeSpan.TotalMinutes}m ago";
        
        return "Just now";
    }
}
