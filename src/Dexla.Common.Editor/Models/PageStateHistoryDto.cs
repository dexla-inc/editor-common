using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class PageStateHistoryDto
{
    public string UserId { get; set; } = string.Empty;
    public long Created { get; set; }
    public List<string> State { get; set; } = [];
    public string? Description { get; set; }
    
    public void SetState(string state)
    {
        const int chunkSize = 100000;
        State = state.SplitStringIntoChunks(chunkSize);
    }
}