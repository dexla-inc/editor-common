using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class VariableModel : IModelWithUserId
{
    internal VariableModel()
    {
    }

    public string? Id { get; set; } = UtilityExtensions.GetId();
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; }
    public string ProjectId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string DefaultValue { get; set; }
    public string Value { get; set; }
    public bool IsGlobal { get; set; }
    public string PageId { get; set; }

    public void SetUserId(string value)
    {
        UserId = value;
    }

    public void SetProjectId(string value)
    {
        ProjectId = value;
    }

    public void SetId()
    {
        Id ??= UtilityExtensions.GetId();
    }
}