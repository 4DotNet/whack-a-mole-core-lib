namespace Wam.Core.Configuration;

public class ServicesConfiguration
{
    public const string SectionName = "Services";

    public string? GamesService { get; set; }
    public string? UsersService { get; set; }
    public string? ScoresService { get; set; }
    public string? VouchersService { get; set; }
    public string? RealtimeService { get; set; }
}