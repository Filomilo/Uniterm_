using Newtonsoft.Json;

namespace Uniterm.Database
{
    public class JsonConverter
    {
        static Newtonsoft.Json.JsonConverter[] converters =
        {
            new UnitermCollectinEntryJsonConverter(),
            new UnitermJsonConverter(),
        };

        public static string ConvertToJson(object obj)
        {
            return JsonConvert.SerializeObject(
                obj,
                Formatting.Indented,
                new JsonSerializerSettings() { Converters = converters }
            );
        }

        public static T ConvertFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(
                json,
                new JsonSerializerSettings() { Converters = converters }
            );
        }
    }
}
