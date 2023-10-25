using System.Text.Json;
using System.Text.Json.Serialization;
using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Types;

public static class Json
{
    public static string Serialize(
        object value,
        Casing casing = Casing.CAMEL_CASE,
        bool ignoreNullValues = false,
        bool writeIndented = false)
    {
        return Serialize(value, _getJsonSerializerOptions(casing, ignoreNullValues, writeIndented));
    }

    private static string Serialize(object value, JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(value, options);
    }

    public static T Deserialize<T>(string value, Casing casing = Casing.CAMEL_CASE, bool ignoreNullValues = false)
    {
        JsonSerializerOptions jsonSerializerSettings = _getJsonSerializerOptions(casing, ignoreNullValues, false);

        return JsonSerializer.Deserialize<T>(value, jsonSerializerSettings) ??
               throw new InvalidOperationException("Value deserialized to null");
    }

    private static JsonSerializerOptions _getJsonSerializerOptions(
        Casing casing,
        bool ignoreNullValues,
        bool writeIndented)
    {
        return new JsonSerializerOptions
        {
            DefaultIgnoreCondition = ignoreNullValues ? JsonIgnoreCondition.WhenWritingNull : JsonIgnoreCondition.Never,
            WriteIndented = writeIndented,
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = casing switch
            {
                Casing.CAMEL_CASE => JsonNamingPolicy.CamelCase,
                Casing.SNAKE_CASE => SnakeCaseNamingPolicy.Instance,
                _ => JsonNamingPolicy.CamelCase
            }
        };
    }
}