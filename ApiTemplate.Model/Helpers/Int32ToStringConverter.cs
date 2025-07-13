using System.Text.Json;
using System.Text.Json.Serialization;

namespace APITemplate.Helpers.Json
{
    public class Int32ToStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string s = reader.GetString();
                    return s;              
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                int? i = reader.GetInt32();
                if (i != null)
                {
                    return i.ToString();
                }
            }

            throw new JsonException("Value could not be converted to string. Value must be an integer or string");
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
