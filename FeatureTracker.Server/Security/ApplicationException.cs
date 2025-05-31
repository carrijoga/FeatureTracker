namespace FeatureTracker.Server.Security;

public class ApplicationException : Exception
{
    public int StatusCode { get; }

    public ApplicationException(string message, int statusCode = StatusCodes.Status400BadRequest)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
