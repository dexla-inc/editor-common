namespace Dexla.Common.Editor.Entities;

public class Action
{
    public string Id { get; set; } = string.Empty;
    public string Trigger { get; set; }= string.Empty;
    public string ActionType { get; set; }= string.Empty;
    public string SequentialTo { get; set; }= string.Empty;
}