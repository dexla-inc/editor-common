namespace Dexla.Common.Editor.Responses;

public class ColorShadeDto : IColor
{
    public string Name { get; set; } = string.Empty;
    public string FriendlyName { get; set; } = string.Empty;
    public string Hex { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
}