using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Uniterm.Models;

namespace Uniterm
{
    public class UnitermCollectinEntryJsonConverter : Newtonsoft.Json.JsonConverter
    {
        static UnitermJsonConverter unitermJsonConverter = new UnitermJsonConverter();

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            UnitermCollectinEntry entry = value as UnitermCollectinEntry;

            writer.WriteStartObject();

            writer.WritePropertyName("UnitermsV");

            writer.WriteStartArray();

            foreach (var item in entry.Collection.VerticalOperations)
            {
                if (item is string)
                    writer.WriteValue(item);
                else if (item is AbstractOperation)
                {
                    unitermJsonConverter.WriteJson(writer, item, serializer);
                }
            }

            writer.WriteEndArray();

            writer.WritePropertyName("UnitermsH");

            writer.WriteStartArray();

            foreach (var item in entry.Collection.HorizontalOperations)
            {
                if (item is string)
                    writer.WriteValue(item);
                else if (item is AbstractOperation)
                {
                    unitermJsonConverter.WriteJson(writer, item, serializer);
                }
            }

            writer.WriteEndArray();

            writer.WritePropertyName("Description");
            writer.WriteValue(entry.Description);
            writer.WritePropertyName("Name");
            writer.WriteValue(entry.Name);
            writer.WriteEndObject();
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            JObject jo = JObject.Load(reader);

            var entry = new UnitermCollectinEntry();
            entry.Collection = new UnitermCollection();

            var unitermsV = jo["UnitermsV"]?.ToObject<List<object>>(serializer);
            if (unitermsV != null)
            {
                entry.Collection.VerticalOperations = new List<AbstractOperation>();
                foreach (var item in unitermsV)
                {
                    if (item is JObject)
                    {
                        AbstractOperation operation =
                            unitermJsonConverter.ReadJson(
                                new JsonTextReader(new StringReader(item.ToString())),
                                typeof(AbstractOperation),
                                null,
                                serializer
                            ) as AbstractOperation;
                        entry.Collection.VerticalOperations.Add(operation);
                    }
                }
            }

            var unitermsH = jo["UnitermsH"]?.ToObject<List<object>>(serializer);
            if (unitermsH != null)
            {
                entry.Collection.HorizontalOperations = new List<AbstractOperation>();
                foreach (var item in unitermsH)
                {
                    if (item is JObject)
                    {
                        AbstractOperation operation =
                            unitermJsonConverter.ReadJson(
                                new JsonTextReader(new StringReader(item.ToString())),
                                typeof(AbstractOperation),
                                null,
                                serializer
                            ) as AbstractOperation;
                        entry.Collection.HorizontalOperations.Add(operation);
                    }
                }
            }

            entry.Description = jo["Description"]?.ToString();
            entry.Name = jo["Name"]?.ToString();

            return entry;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UnitermCollectinEntry);
        }
    }
}
