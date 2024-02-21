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
    public string ProjectId { get; private set; } = string.Empty;
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
        const int chunkSize = 2;
        State = SplitStringIntoChunks(state, chunkSize);
    }

    // Remove after next build of common.
    private static List<string> SplitStringIntoChunks(string source, int chunkSize)
    {
        List<string> chunks = [];

        for (int i = 0; i < source.Length; i += chunkSize)
            chunks.Add(i + chunkSize <= source.Length ? source.Substring(i, chunkSize) : source[i..]);

        return chunks;
    }
}