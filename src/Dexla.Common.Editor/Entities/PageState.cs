using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Entities;

public class PageState : IPageEntity
{
    protected PageState()
    {
    }

    public virtual string Id { get; set; } = UtilityExtensions.GetId();
    public virtual EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }
    public virtual string UserId { get; set; } = string.Empty;

    public virtual string ProjectId { get; set; } = string.Empty;
    public virtual string PageId { get; set; } = string.Empty;
    public virtual List<string> State { get; set; } = [];
    public virtual long Created { get; set; }
    public IPageEntity SetNewValues(string userId, string projectId, string pageId)
    {
        UserId = userId;
        ProjectId = projectId;
        PageId = pageId;
        Id = pageId;
        return this;
    }
}