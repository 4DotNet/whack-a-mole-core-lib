using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wam.Core.Helpers;

public class SystemTextSerializationHelper : ISerializationHelper
{
    private readonly JsonSerializerOptions options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IgnoreReadOnlyFields = true,
        PropertyNameCaseInsensitive = true,
        AllowTrailingCommas = true,
        Converters = { new JsonStringEnumConverter() },
        MaxDepth = 5,
        WriteIndented = false
    };

    public string Serialize<T>(T obj)
    {
        return System.Text.Json.JsonSerializer.Serialize(obj, options);
    }

    public T? Deserialize<T>(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(json,options);
    }
}