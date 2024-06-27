using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Entities;

public class Variable : IProjectEntity
{
    protected Variable()
    {
    }

    public string Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }
    public string UserId { get; set; }
    public string ProjectId { get; set; }
    public string Name { get; set; }
    public FrontEndTypes Type { get; set; }
    public string DefaultValue { get; set; }
    public bool IsGlobal { get; set; }
    public IProjectEntity SetNewValues(string userId, string projectId)
    {
        UserId = userId;
        ProjectId = projectId;
        Id = UtilityExtensions.GetId();
        return this;
    }
}