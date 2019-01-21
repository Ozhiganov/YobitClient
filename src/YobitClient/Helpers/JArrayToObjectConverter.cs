using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YobitClient.Helpers
{
    public class JArrayToObjectConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            existingValue = existingValue ?? Activator.CreateInstance(objectType);
            var array = JArray.Load(reader);
            FieldInfo[] fields = objectType.GetRuntimeFields().ToArray();
            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo field = fields[i];
                JToken token = array[i];
                field.SetValue(existingValue, token.ToObject(field.FieldType));
            }
            return existingValue;
        }

        public override bool CanConvert(Type objectType) => false;
    }
}
