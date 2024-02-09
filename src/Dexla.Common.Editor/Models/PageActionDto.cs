namespace Dexla.Common.Editor.Models;

public class PageActionDto
{
    public string Id { get; set; } = string.Empty;
    public string Trigger { get; set; }= string.Empty;
    public string Action { get; set; } = string.Empty;
    public string SequentialTo { get; set; }= string.Empty;
}