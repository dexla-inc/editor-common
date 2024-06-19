using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dexla.Common.Utilities;

public class IgnoreNullValuesConverter<T> : JsonConverter<T>
{
    private readonly JsonSerializerOptions _writeOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private readonly JsonSerializerOptions _readOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<T>(ref reader, _readOptions);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, _writeOptions);
    }
}