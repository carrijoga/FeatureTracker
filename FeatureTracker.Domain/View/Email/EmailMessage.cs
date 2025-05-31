namespace FeatureTracker.Domain.View.Email;

public class EmailMessage
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public string From { get; set; }
    public string FromName { get; set; }
    public string ReplyTo { get; set; }
    public string ReplyToName { get; set; }
}
