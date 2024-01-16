using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Wam.Core.Events;

public class RealtimeEvent<T>
{
    public string Message { get; set; }
    public T Data { get; set; }

    public string ToJson()
    {
        var contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.None
        });

    }
}