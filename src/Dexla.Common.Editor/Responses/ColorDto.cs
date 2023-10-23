namespace Dexla.Common.Editor.Responses;

public class ColorDto
{
    public string Name { get; set; }= string.Empty;
    public string FriendlyName { get; set; }= string.Empty;
    public string Hex { get; set; }= string.Empty;
    public int Brightness { get; set; }
    public bool IsDefault { get; set; }
}