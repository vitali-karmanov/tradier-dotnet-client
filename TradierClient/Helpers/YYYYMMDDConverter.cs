using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Tradier.Client.Helpers
{
    public class YYYYMMDDConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }
            return DateTime.ParseExact(reader.Value.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
