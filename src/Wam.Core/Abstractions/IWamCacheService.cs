namespace Wam.Core.Abstractions;

public interface IWamCacheService
{
    Task<T> GetFromCacheOrInitialize<T>(string cacheKey, Func<Task<T>> initializeFunction, int ttlInSeconds = 900,
        CancellationToken cancellationToken = default);
    Task SaveState<T>(string cacheKey, T data, int ttlInSeconds = 900, CancellationToken? cancellationToken = null);
    Task Invalidate(string cacheKey, CancellationToken? cancellationToken = null);
}