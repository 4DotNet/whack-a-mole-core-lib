using HexMaster.RedisCache;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wam.Core.Identity;

namespace Wam.Core.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWamCoreConfiguration(
        this IServiceCollection services,
        IConfiguration configuration,
        bool skipApplicationInsights = false)
    {
        var azureServicesOptions = configuration.GetSection(AzureServices.SectionName).Get<AzureServices>();
        services.AddHealthChecks();
        services.AddOptions<AzureServices>().Bind(configuration.GetSection(AzureServices.SectionName)); //.ValidateOnStart();
        services.AddAzureClients(builder =>
        {
            builder.AddWebPubSubServiceClient(
                new Uri(azureServicesOptions.WebPubSubEndpoint),
                azureServicesOptions.WebPubSubHub,
                CloudIdentity.GetCloudIdentity());
        });

        if (!skipApplicationInsights)
        {
            services.AddApplicationInsightsTelemetry(configuration);
        }

        services.AddHexMasterCache(configuration);
        return services;
    }
}