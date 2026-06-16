using GlasFlex.Website.Contract.Ratelimiter;

namespace GlasFlex.Website.Domain.Ratelimiter;

public interface IRateLimitStore
{
    ValueTask<int> GetPointsAsync(string storeName, string key);
    ValueTask AddPointsAsync(string storeName, string key, RatelimitScore score);
    ValueTask ResetAsync(string storeName, string key);
}