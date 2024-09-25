namespace Dexla.Common.Editor.Responses;

public interface IColor
{
    public string Name { get; set; }
    public string FriendlyName { get; set; }
    public string Hex { get; set; }
    public bool IsDefault { get; set; }
}