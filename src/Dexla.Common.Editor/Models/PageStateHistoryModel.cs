using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class PageStateHistoryModel : IModelWithProjectId
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }

    public void SetCompanyId(string value)
    {
        CompanyId = value;
    }

    public string UserId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string PageId { get; set; } = string.Empty;
    public List<string> State { get; set; } = [];
    public long Created { get; set; } 
    public string? Description { get; set; }
    
    public void SetProjectId(string projectId)
    {
        ProjectId = projectId;
    }

    public void SetUserId(string value)
    {
        UserId = value;
    }

    public void SetPageId(string pageId)
    {
        PageId = pageId;
    }

    public void SetState(string state)
    {
        const int chunkSize = 10000;
        State = state.SplitStringIntoChunks(chunkSize);
        Created = DateTimeExtensions.GetTimestamp();
    }
}