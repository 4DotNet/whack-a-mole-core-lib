using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wam.Core.Helpers;
public interface ISerializationHelper
{
    string Serialize<T>(T obj);
    T? Deserialize<T>(string json);
}

public class JsonSerializationHelper : ISerializationHelper
{
    public string Serialize<T>(T obj)
    {
        var contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy(),            
        };
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.None
        });
    }

    public T? Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}

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