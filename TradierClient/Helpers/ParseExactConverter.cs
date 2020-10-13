using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Tradier.Client.Helpers
{
    public class ParseExactConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(("\"" + (DateTime)value).ToString() + "\"");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }
            return DateTime.ParseExact(reader.Value.ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
