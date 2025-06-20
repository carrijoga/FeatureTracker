namespace FeatureTracker.Application.Services.LoginAttempt;

public interface ILoginAttemptService
{
    Task<int> GetAttemptsAsync(string userKey);
    Task IncrementAsync (string userKey);
    Task ResetAsync(string userKey);
    Task<bool> IsLockedOutAsync(string userKey, int maxAttempts);
}
