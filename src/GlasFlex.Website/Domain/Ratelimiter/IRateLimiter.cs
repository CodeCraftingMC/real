using GlasFlex.Website.Contract.Ratelimiter;

namespace GlasFlex.Website.Domain.Ratelimiter;


public interface IRateLimiter
{
    ValueTask<int> GetPointsAsync(string storeName, string key);
    ValueTask<bool> IsLimitedAsync(string storeName, string key, int maxPoints);
    ValueTask<int> AddAsync(string storeName, string key, RatelimitScore score);
    ValueTask ResetAsync(string storeName, string key);
}