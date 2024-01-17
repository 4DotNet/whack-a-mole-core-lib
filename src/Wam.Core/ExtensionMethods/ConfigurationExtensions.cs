using Microsoft.Extensions.Configuration;

namespace Wam.Core.ExtensionMethods;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration configuration, string key, string? description = null)
    {
        var value = configuration.GetValue<string>(key)??string.Empty;
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException("Missing setting " + ((description != null) ? ("for " + description) : "") + " : " + key);
        }

        return value;
    }
}