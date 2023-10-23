using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class VariableResponse : ISuccess
{
    public string Id { get; }
    public string Name { get; }
    public FrontEndTypes Type { get; }
    public string DefaultValue { get; }
    public string Value { get; }
    public bool IsGlobal { get; }
    public string PageId { get; }

    public VariableResponse(
        string id,
        string name,
        FrontEndTypes type,
        string defaultValue,
        string value,
        bool isGlobal,
        string pageId)
    {
        Id = id;
        Name = name;
        Type = type;
        DefaultValue = defaultValue;
        Value = value;
        IsGlobal = isGlobal;
        PageId = pageId;
    }

    internal VariableResponse()
    {
        
    }

    public string TrackingId { get; set; }
}