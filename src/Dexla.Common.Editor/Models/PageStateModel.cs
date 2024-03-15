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
    public List<PageStateHistoryDto> History { get; set; } = [];
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
        const int chunkSize = 100000;
        State = state.SplitStringIntoChunks(chunkSize);
        History.Add(new PageStateHistoryDto
        {
            UserId = UserId,
            Created = DateTimeExtensions.GetTimestamp(),
            State = State
        });
    }
}