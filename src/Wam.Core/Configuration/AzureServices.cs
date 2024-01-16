namespace Wam.Core.Configuration;

public class AzureServices
{
    public const string SectionName = "AzureServices";
    public string WebPubSubEndpoint { get; set; }
    public string WebPubSubHub{ get; set; }
    public string UsersStorageAccountName { get; set; }
}