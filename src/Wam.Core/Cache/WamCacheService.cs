using Dapr.Client;
using Microsoft.Extensions.Logging;
using Wam.Core.Abstractions;

namespace Wam.Core.Cache;

public class WamCacheService(
    ILogger<WamCacheService> logger,
    DaprClient dapr) : IWamCacheService
{

    private const string StateStoreName = "statestore";

    public async Task<T> GetFromCacheOrInitialize<T>(string cacheKey, Func<Task<T>> initializeFunction, int ttlInSeconds = 900, CancellationToken cancellationToken = default )
    {
        try
        {
            var state = await dapr.GetStateEntryAsync<T>(
                StateStoreName,
                cacheKey,
                cancellationToken: cancellationToken);

            if (state.Value != null)
            {
                logger.LogInformation("Received information from cache ({cacheKey})", cacheKey);
                return state.Value;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get state from cache");
        }

        var tResult = await initializeFunction();

        try
        {
            if (tResult != null)
            {
                await SaveState(cacheKey, tResult, ttlInSeconds, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to save state to cache");
        }

        return tResult;
    }

    public async Task SaveState<T>(string cacheKey, T data, int ttlInSeconds = 900,  CancellationToken? cancellationToken = null)
    {

        var usableCancellationToken = cancellationToken ?? CancellationToken.None;
        await dapr.SaveStateAsync(
            StateStoreName,
            cacheKey,
            data,
            metadata: new Dictionary<string, string>
            {
                {
                    "ttlInSeconds", $"{ttlInSeconds}"
                }
            },
            cancellationToken: usableCancellationToken);
    }

    public async Task Invalidate(string cacheKey, CancellationToken? cancellationToken = null)
    {
        try
        {
            var usableCancellationToken = cancellationToken ?? CancellationToken.None;
            logger.LogInformation("Invalidating cache key {cacheKey}", cacheKey);
            await dapr.DeleteStateAsync(StateStoreName, cacheKey, cancellationToken: usableCancellationToken);
            logger.LogInformation("Cache key {cacheKey} invalidated successfully", cacheKey);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to invalidate cache key {cacheKey}", cacheKey);
        }
    }
}