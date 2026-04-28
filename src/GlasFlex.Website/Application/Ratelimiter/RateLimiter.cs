using GlasFlex.Website.Contract.Ratelimiter;
using GlasFlex.Website.Domain.Ratelimiter;

namespace GlasFlex.Website.Application.Ratelimiter;

public sealed class RateLimiter : IRateLimiter
{
    private readonly IRateLimitStore _store;

    public RateLimiter(IRateLimitStore store)
    {
        _store = store;
    }

    public ValueTask<int> GetPointsAsync(string storeName, string key)
    {
        return _store.GetPointsAsync(storeName, key);
    }

    public async ValueTask<bool> IsLimitedAsync(string storeName, string key, int maxPoints)
    {
        Console.WriteLine($"Checking rate limit for Store: {storeName}, Key: {key}, MaxPoints: {maxPoints}, current: {await _store.GetPointsAsync(storeName, key)}");
        return await _store.GetPointsAsync(storeName, key) >= maxPoints;
    }

    public async ValueTask<int> AddAsync(string storeName, string key, RatelimitScore score)
    {
        await _store.AddPointsAsync(storeName, key, score);
        return await _store.GetPointsAsync(storeName, key);
    }

    public ValueTask ResetAsync(string storeName, string key)
    {
        return _store.ResetAsync(storeName, key);
    }
}