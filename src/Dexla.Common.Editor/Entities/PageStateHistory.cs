namespace Dexla.Common.Editor.Entities;

public class PageStateHistory
{
    public string UserId { get; set; } = string.Empty;
    public long Created { get; set; }
    public List<string> State { get; set; } = [];
    public string? Description { get; set; }
}