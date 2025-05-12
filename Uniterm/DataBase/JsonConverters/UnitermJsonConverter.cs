using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Uniterm.Models;

namespace Uniterm.Database
{
    internal class UnitermJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var operation = (AbstractOperation)value;

            writer.WriteStartObject();

            writer.WritePropertyName("Seprator");
            writer.WriteValue(operation.Separator);

            writer.WritePropertyName("Direction");
            writer.WriteValue(operation.Direction);

            writer.WritePropertyName("ExpressionA");
            if (operation.ExpressionA is string)
                writer.WriteValue(operation.ExpressionA);
            else if (operation.ExpressionA is AbstractOperation)
            {
                this.WriteJson(writer, operation.ExpressionA, serializer);
            }

            writer.WritePropertyName("ExpressionB");
            if (operation.ExpressionB is string)
                writer.WriteValue(operation.ExpressionB);
            else if (operation.ExpressionB is AbstractOperation)
            {
                this.WriteJson(writer, operation.ExpressionB, serializer);
            }
            OperationType type = OperationFactory.GetOperationType(operation);

            writer.WritePropertyName("Type");
            writer.WriteValue(type);

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

            OperationType type = (OperationType)(int)jo["Type"];
            string seprator = jo["Seprator"]?.ToString();
            DirectionEnum Direction = (DirectionEnum)(int)jo["Direction"];

            object ExpressoinA = null;
            JToken exprA = jo["ExpressionA"];
            if (exprA != null)
            {
                if (exprA.Type == JTokenType.Object)
                {
                    ExpressoinA = exprA.ToObject<AbstractOperation>(serializer);
                }
                else
                {
                    ExpressoinA = exprA.ToString();
                }
            }
            object ExpressoinB = null;

            JToken exprB = jo["ExpressionB"];
            if (exprB != null)
            {
                if (exprB.Type == JTokenType.Object)
                {
                    ExpressoinB = exprB.ToObject<AbstractOperation>(serializer);
                }
                else
                {
                    ExpressoinB = exprB.ToString();
                }
            }

            return OperationFactory.CreateOperation(
                type,
                ExpressoinA,
                ExpressoinB,
                seprator,
                Direction
            );
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AbstractOperation);
        }
    }
}
