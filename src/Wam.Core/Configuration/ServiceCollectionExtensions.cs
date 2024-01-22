using HexMaster.RedisCache;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Wam.Core.Identity;

namespace Wam.Core.Configuration;

public static class ServiceCollectionExtensions
{
    private const string MissingConfigMessage = "Missing Azure Services configuration";

    public static IServiceCollection AddWamCoreConfiguration(
        this IServiceCollection services,
        [NotNull] IConfiguration configuration,
        bool skipApplicationInsights = false)
    {
        var azureServicesOptions = configuration
            .GetSection(AzureServices.SectionName)
            .Get<AzureServices>()
            ?? throw new InvalidOperationException(MissingConfigMessage);

        services.AddHealthChecks();
        services.AddOptions<AzureServices>().Bind(configuration.GetSection(AzureServices.SectionName));
        services.AddOptions<ServicesConfiguration>().Bind(configuration.GetSection(ServicesConfiguration.SectionName));

        string webPubSubEndpoint =
            azureServicesOptions.WebPubSubEndpoint
            ?? throw new InvalidOperationException(MissingConfigMessage);

        services.AddAzureClients(builder =>
        {
            builder.AddWebPubSubServiceClient(
                new Uri(webPubSubEndpoint),
                azureServicesOptions.WebPubSubHub,
                CloudIdentity.GetCloudIdentity);
        });

        if (!skipApplicationInsights)
        {
            services.AddApplicationInsightsTelemetry(configuration);
        }

        services.AddHexMasterCache(configuration);
        return services;
    }
}