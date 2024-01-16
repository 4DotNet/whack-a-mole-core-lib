using Microsoft.Extensions.Configuration;

namespace Wam.Core.ExtensionMethods;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration configuration, string key, string? description = null)
    {
        string value = configuration.GetValue<string>(key);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException("Missing setting " + ((description != null) ? ("for " + description) : "") + " : " + key);
        }

        return value;
    }
}