namespace Dexla.Common.Editor.Models;

public class AppDto
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public object Configuration { get; set; } = new object();
}