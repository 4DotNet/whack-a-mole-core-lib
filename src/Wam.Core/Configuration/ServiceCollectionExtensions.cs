using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wam.Core.Identity;

namespace Wam.Core.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var positionOptions = configuration.GetSection(AzureServices.SectionName).Get<AzureServices>();
        services.AddOptions<AzureServices>().Bind(configuration.GetSection(AzureServices.SectionName)); //.ValidateOnStart();
        services.AddAzureClients(builder =>
        {
            builder.AddWebPubSubServiceClient(new Uri(positionOptions.WebPubSubEndpoint),
                positionOptions.WebPubSubHub, CloudIdentity.GetCloudIdentity());
        });
        return services;
    }
}