namespace Wam.Core.Cache;

public static class CacheName
{
    public static string UserDetails(Guid userId) => $"UserDetails:{userId:N}";

    public static string GameDetails(Guid userId) => $"GameDetails:Id:{userId:N}";
    public static string GameDetails(string gameCode) => $"GameDetails:Code:{gameCode}";
}