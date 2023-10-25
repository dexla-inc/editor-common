using System.Text.Json.Serialization;

namespace Dexla.Common.Types.Models;

public class ApiRequest
{
    [JsonPropertyOrder(7)]
    public string? Id { get; set; }
    [JsonPropertyOrder(1)]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyOrder(2)]
    public string AuthenticationScheme { get; set; } = string.Empty;
    [JsonPropertyOrder(3)]
    public string Environment { get; set; } = string.Empty;
    [JsonPropertyOrder(4)]
    public string BaseUrl { get; set; } = string.Empty;
    [JsonPropertyOrder(5)]
    public string? SwaggerUrl { get; set; }
    [JsonPropertyOrder(6)]
    public string? AuthValue { get; set;  }
    [JsonPropertyOrder(7)]
    public bool IsTested { get; set; }
}

public class ApiUpdateRequest : ApiRequest
{
    
}