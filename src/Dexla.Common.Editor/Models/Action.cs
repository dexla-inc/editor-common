namespace Dexla.Common.Editor.Models;

public class ActionDto
{
    public string Id { get; set; } = string.Empty;
    public string Trigger { get; set; }= string.Empty;
    public string ActionType { get; set; }= string.Empty;
    public string SequentialTo { get; set; }= string.Empty;
}