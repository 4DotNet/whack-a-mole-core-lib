namespace Wam.Core.Helpers;
public interface ISerializationHelper
{
    string Serialize<T>(T obj);
    T? Deserialize<T>(string json);
}
