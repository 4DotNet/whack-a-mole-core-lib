namespace Wam.Core.Cache;

public static class CacheName
{
    public static string UserDetails(Guid userId) => $"wam:user:id:{userId:N}";
    public static string GameDetails(Guid gameId) => $"wam:game:id:{gameId:N}";
    public static string GameDetails(string gameCode) => $"wam:game:code:{gameCode}";
    public static string GameScoreBoard(Guid gameId) => $"wam:game:scoreboard:{gameId:N}";
    public static string VoucherInfo(Guid voucherId) => $"wam:vouchers:{voucherId:N}";
}