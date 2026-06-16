using GlasFlex.Website.Contract.Ratelimiter;
using GlasFlex.Website.Domain.Ratelimiter;

namespace GlasFlex.Website.Application.Ratelimiter;

public sealed class InMemoryRateLimitStore : IRateLimitStore
{
    private readonly Dictionary<string, List<Entry>> _entries = new();
    private readonly object _lock = new();

    public ValueTask<int> GetPointsAsync(string storeName, string key)
    {
        var now = DateTimeOffset.UtcNow;
        var storeKey = BuildStoreKey(storeName, key);

        lock (_lock)
        {
            if (!_entries.TryGetValue(storeKey, out var entries))
                return ValueTask.FromResult(0);

            entries.RemoveAll(x => x.ExpiresAt <= now);

            if (entries.Count == 0)
            {
                _entries.Remove(storeKey);
                return ValueTask.FromResult(0);
            }

            return ValueTask.FromResult(entries.Sum(x => x.Points));
        }
    }

    public ValueTask AddPointsAsync(string storeName, string key, RatelimitScore score)
    {
        if (score.Points <= 0 || score.Duration <= TimeSpan.Zero)
            return ValueTask.CompletedTask;

        var now = DateTimeOffset.UtcNow;
        var storeKey = BuildStoreKey(storeName, key);

        lock (_lock)
        {
            if (!_entries.TryGetValue(storeKey, out var entries))
            {
                entries = [];
                _entries[storeKey] = entries;
            }

            entries.RemoveAll(x => x.ExpiresAt <= now);

            entries.Add(new Entry(
                score.Points,
                now.Add(score.Duration)));
        }

        return ValueTask.CompletedTask;
    }

    public ValueTask ResetAsync(string storeName, string key)
    {
        lock (_lock)
        {
            _entries.Remove(BuildStoreKey(storeName, key));
        }

        return ValueTask.CompletedTask;
    }

    private static string BuildStoreKey(string storeName, string key)
    {
        return $"{storeName.Trim()}:{key.Trim()}";
    }

    private sealed record Entry(int Points, DateTimeOffset ExpiresAt);
}