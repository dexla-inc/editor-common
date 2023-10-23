using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Entities;

public class EndpointAuthentication
{
    public EndpointTypes EndpointType { get; set; }
    public string TokenKey { get; set; } = string.Empty;
    public string? TokenSecondaryKey { get; set; }
}