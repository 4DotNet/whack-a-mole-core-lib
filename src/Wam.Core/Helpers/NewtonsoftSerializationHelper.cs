using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Wam.Core.Helpers;

public class NewtonsoftSerializationHelper : ISerializationHelper
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
