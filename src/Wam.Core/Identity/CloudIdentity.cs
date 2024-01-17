using Azure.Core;
using Azure.Identity;

namespace Wam.Core.Identity;

public static class CloudIdentity
{
    public static TokenCredential GetCloudIdentity =>  new ChainedTokenCredential(
            new ManagedIdentityCredential(),
            new VisualStudioCredential(),
            new AzureCliCredential());    
}