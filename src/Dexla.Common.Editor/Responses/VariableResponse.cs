using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class VariableResponse : ISuccess
{
    public string Id { get; }
    public string Name { get; }
    public FrontEndTypes Type { get; }
    public string DefaultValue { get; }

    public VariableResponse(
        string id,
        string name,
        FrontEndTypes type,
        string defaultValue)
    {
        Id = id;
        Name = name;
        Type = type;
        DefaultValue = defaultValue;
    }

    internal VariableResponse()
    {
        
    }

    public string TrackingId { get; set; }
}