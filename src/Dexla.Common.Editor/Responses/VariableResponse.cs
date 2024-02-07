using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class VariableResponse : ISuccess
{
    public string Id { get; }
    public string Name { get; }
    public FrontEndTypes Type { get; }
    public string DefaultValue { get; }
    public bool IsGlobal { get; }

    public VariableResponse(
        string id,
        string name,
        FrontEndTypes type,
        string defaultValue,
        bool isGlobal)
    {
        Id = id;
        Name = name;
        Type = type;
        DefaultValue = defaultValue;
        IsGlobal = isGlobal;
    }

    internal VariableResponse()
    {
        
    }

    public string TrackingId { get; set; }
}