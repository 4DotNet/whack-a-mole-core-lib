namespace Wam.Core.Configuration;

public class AzureServices
{
    public const string SectionName = "AzureServices";
    public string WebPubSubEndpoint { get; set; } = null!;
    public string WebPubSubHub{ get; set; } = null!;
    public string UsersStorageAccountName { get; set; } = null!;
    public string GamesStorageAccountName { get; set; } = null!;
    public string ScoresStorageAccountName { get; set; } = null!;
    public string VouchersStorageAccountName { get; set; } = null!;
}