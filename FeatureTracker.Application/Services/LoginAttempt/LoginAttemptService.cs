using StackExchange.Redis;

namespace FeatureTracker.Application.Services.LoginAttempt;

public class LoginAttemptService : ILoginAttemptService
{
    #region Dependencies
    private readonly IDatabase _redis;
    private readonly TimeSpan _resetTime = TimeSpan.FromMinutes(15);
    #endregion

    #region Constructor
    public LoginAttemptService(IConnectionMultiplexer redis) =>
        _redis = redis.GetDatabase();
    #endregion

    #region Properties
    private string GetRedisKey(string userKey) =>
        $"login:attempts:{userKey.ToLower()}";
    #endregion

    #region Methods
    public async Task<int> GetAttemptsAsync(string userKey)
    {
        var attempts = await _redis.StringGetAsync(GetRedisKey(userKey));
        return attempts.HasValue ? (int)attempts : 0;
    }

    public async Task IncrementAsync(string userKey)
    {
        var count = await _redis.StringIncrementAsync(GetRedisKey(userKey));
        if (count == 1)
            await _redis.KeyExpireAsync(GetRedisKey(userKey), _resetTime);
    }
    public async Task<bool> IsLockedOutAsync(string userKey, int maxAttempts) =>
        await GetAttemptsAsync(userKey) >= maxAttempts;

    public async Task ResetAsync(string userKey) =>
        await _redis.KeyDeleteAsync(GetRedisKey(userKey));
    #endregion
}
