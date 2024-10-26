using Dexla.Common.Types.Enums;

namespace Dexla.Common.Types.Models;

public class EndpointAuthentication
{
    public EndpointTypes EndpointType { get; set; }
    public string TokenKey { get; set; } = string.Empty;
    public string? TokenSecondaryKey { get; set; }
}