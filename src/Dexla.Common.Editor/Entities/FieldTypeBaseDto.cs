using System.Text.Json.Serialization;

namespace Dexla.Common.Editor.Entities;

public class FieldTypeBaseDto
{
    [JsonPropertyOrder(1)]
    public string? Name { get; set; } = string.Empty;

    [JsonPropertyOrder(2)]
    public string? Type { get; set; } = string.Empty;

    [JsonPropertyOrder(3)]
    public string? Description { get; set; }

    [JsonPropertyOrder(4)]
    public string? Value { get; set; }
}