namespace Dexla.Common.Editor.Entities;

public class PageAction
{
    public string Id { get; set; } = string.Empty;
    public string Trigger { get; set; }= string.Empty;
    public string Action { get; set; } = string.Empty;
    public string SequentialTo { get; set; }= string.Empty;
}