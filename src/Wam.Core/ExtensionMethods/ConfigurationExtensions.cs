using Microsoft.Extensions.Configuration;

namespace Wam.Core.ExtensionMethods;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration configuration, string key, string? description = null)
    {
        return configuration.GetValue<string>(key)
            ?? throw new InvalidOperationException(
                $"Missing setting {ForDescription(description)}: {key}");
    }

    private static string ForDescription(string? description) => (description != null) ? $"for {description} " : string.Empty;
}