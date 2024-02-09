using System.Reflection;
using HexMaster.RedisCache;
using Man.Dapr.Sidekick;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders.Testing;
using Wam.Core.ErrorCodes;
using Wam.Core.Exceptions;
using Wam.Core.Identity;

namespace Wam.Core.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWamCoreConfiguration(
        this IServiceCollection services,
        IConfiguration configuration,
        bool skipApplicationInsights = false,
        string? daprAppId = null)
    {
        services.AddHealthChecks();

        var azureServicesSection = configuration.GetSection(AzureServices.SectionName);
        var azureServices = azureServicesSection.Get<AzureServices>();
        services.AddOptions<AzureServices>().Bind(azureServicesSection); //.ValidateOnStart();

        var wamServicesSection= configuration.GetSection(ServicesConfiguration.SectionName);
        var wamServices = wamServicesSection.Get<ServicesConfiguration>();
        services.AddOptions<ServicesConfiguration>().Bind(wamServicesSection); //.ValidateOnStart();



        if (!string.IsNullOrWhiteSpace(daprAppId))
        {
            var daprAppServiceProperty = wamServices.GetType().GetProperty(daprAppId);
            var propertyValue = daprAppServiceProperty.GetValue(wamServices);

            services.AddDaprSidekick(config =>
            {
                config.Sidecar = new DaprSidecarOptions()
                {
                    AppId = propertyValue.ToString()
                };
            });
        }


        services.AddAzureClients(builder =>
        {
            builder.AddWebPubSubServiceClient(
                new Uri(azureServices.WebPubSubEndpoint),
                azureServices.WebPubSubHub,
                CloudIdentity.GetCloudIdentity());
        });

        if (!skipApplicationInsights)
        {
            services.AddApplicationInsightsTelemetry(configuration);
        }

        services.AddHexMasterCache(configuration);
        services.AddDaprClient();
        return services;
    }
}