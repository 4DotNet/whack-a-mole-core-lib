namespace Wam.Core.Configuration;

public class AzureServices
{
    public const string SectionName = "AzureServices";
    public string WebPubSubEndpoint { get; set; } = string.Empty;
    public string WebPubSubHub{ get; set; } = string.Empty;
    public string UsersStorageAccountName { get; set; } = string.Empty;
    public string GamesStorageAccountName { get; set; } = string.Empty;
}