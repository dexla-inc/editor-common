using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Responses;

public class FontDto
{
    public FontTypes Type { get; set; }
    public string FontFamily { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string FontWeight { get; set; } = string.Empty;
    public string FontSize { get; set; } = string.Empty;
    public string LineHeight { get; set; } = string.Empty;
    public string LetterSpacing { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}