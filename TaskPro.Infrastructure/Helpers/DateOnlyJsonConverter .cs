using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskPro.Infrastructure.Helpers
{
    public class DateOnlyJsonConverter : JsonConverter<DateTime>
    {
        private readonly string _format = "yyyy-MM-dd";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Lee la fecha desde string sin hora
            var value = reader.GetString();
            if (DateTime.TryParseExact(value, _format, null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }
            return DateTime.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Escribe solo la fecha en formato yyyy-MM-dd
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
