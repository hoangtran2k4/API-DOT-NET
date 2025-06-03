using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonDateOnlyConverter : JsonConverter<DateTime?>
{
    private readonly string _format = "yyyy-MM-dd";

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return string.IsNullOrWhiteSpace(value) ? null : DateTime.ParseExact(value, _format, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(_format));
        else
            writer.WriteNullValue();
    }
}
    