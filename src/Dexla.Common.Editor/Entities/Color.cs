namespace Dexla.Common.Editor.Entities;

public class Color
{
    public string Name { get; set; }
    public string FriendlyName { get; set; }
    public string Hex { get; set; }
    public int Brightness { get; set; }
    public bool IsDefault { get; set; }

    public Color(string name, string friendlyName, string hex, int brightness, bool isDefault)
    {
        Name = name;
        FriendlyName = friendlyName;
        Hex = hex;
        Brightness = brightness;
        IsDefault = isDefault;
    }
}