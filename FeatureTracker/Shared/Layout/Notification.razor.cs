using Microsoft.AspNetCore.Components;

namespace FeatureTracker.Client.Shared.Layout;

public class NotificationComponentBase : ComponentBase
{
    protected bool Open;
    protected List<TwoStringItems> Content { get; set; }

    protected void ToggleOpen()
    {
        Open = !Open;

        VerifyNotificationContentList();
    }

    public void VerifyNotificationContentList()
    {
        Content ??= [];
        if (Content.Count > 1)
            Content = new List<TwoStringItems>();
    }


    public void AddMoreContent()
    {
        var newContent =
            new TwoStringItems(Guid.NewGuid(), "New Notification! 💻", "Scroll your browser to see effect.");
        Content.Add(newContent);
    }

    public void RemoveContent(Guid id)
    {
        var item = Content.FirstOrDefault(x => x.Id == id);
        Content.Remove(item);
    }
}

public record TwoStringItems(Guid Id, string Title, string Subtitle);
