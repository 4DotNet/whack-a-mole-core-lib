namespace Wam.Core.Configuration;

public class AzureServices
{
    public const string SectionName = "AzureServices";
    public string StorageAccountName { get; set; }
    public string WebPubSubEndpoint { get; set; }
    public string WebPubSubHub{ get; set; }
}