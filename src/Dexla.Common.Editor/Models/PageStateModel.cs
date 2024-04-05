using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class PageStateModel : IModelWithUserId
{
    public string? Id
    {
        get => PageId;
        set
        {
            if (value != null) PageId = value;
        }
    }
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string PageId { get; set; } = string.Empty;
    public List<string> State { get; set; } = [];
    public long Created { get; set; } = DateTimeExtensions.GetTimestamp();

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
    }
    
    public static PageStateModel Empty(string userId, string projectId, string pageId)
    {
        return new PageStateModel
        {
            UserId = userId,
            ProjectId = projectId,
            PageId = pageId,
            EntityStatus = EntityStatus.Active,
            State = [],
            Created = DateTimeExtensions.GetTimestamp()
        };
    }
}