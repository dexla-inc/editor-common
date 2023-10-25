using System.Text.Json.Serialization;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Types.Models;

public class ApiRequestResponse : ApiRequest, ISuccess
{
    [JsonPropertyOrder(6)]
    public List<ApiEndpointOpenApi> Endpoints { get; set; } = new();

    public string TrackingId { get; set; }
}