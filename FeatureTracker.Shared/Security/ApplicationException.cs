namespace FeatureTracker.Shared.Security;

public class ApplicationException : Exception
{
    public int StatusCode { get; }

    public ApplicationException(string message, int statusCode = 400)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
