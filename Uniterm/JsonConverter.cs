using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Uniterm
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
