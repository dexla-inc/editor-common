using Dexla.Common.Editor.Responses;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Models;

public static class Fonts
{
    public const string DefaultFontFamily = "Open Sans";

    public static readonly List<FontDto> DefaultFonts =

    [
        new FontDto
        {
            Type = FontTypes.TITLE,
            FontFamily = DefaultFontFamily,
            Tag = "H1",
            FontWeight = "500",
            FontSize = "32px",
            LineHeight = "48px",
            LetterSpacing = "0px",
            Note = "Main Title"
        },
        new FontDto
        {
            Type = FontTypes.TITLE,
            FontFamily = DefaultFontFamily,
            Tag = "H2",
            FontWeight = "500",
            FontSize = "28px",
            LineHeight = "42px",
            LetterSpacing = "0px",
            Note = "Section Title"
        },
        new FontDto
        {
            Type = FontTypes.TITLE,
            FontFamily = DefaultFontFamily,
            Tag = "H3",
            FontWeight = "500",
            FontSize = "24px",
            LineHeight = "40px",
            LetterSpacing = "0px",
            Note = "Subsection Title"
        },
        new FontDto
        {
            Type = FontTypes.TITLE,
            FontFamily = DefaultFontFamily,
            Tag = "H4",
            FontWeight = "600",
            FontSize = "22px",
            LineHeight = "32px",
            LetterSpacing = "0px",
            Note = "Topic Title"
        },
        new FontDto
        {
            Type = FontTypes.TITLE,
            FontFamily = DefaultFontFamily,
            Tag = "H5",
            FontWeight = "500",
            FontSize = "20px",
            LineHeight = "24px",
            LetterSpacing = "0px",
            Note = "Subtopic Title"
        },
        new FontDto
        {
            Type = FontTypes.TITLE,
            FontFamily = DefaultFontFamily,
            Tag = "H6",
            FontWeight = "500",
            FontSize = "18px",
            LineHeight = "24px",
            LetterSpacing = "0px",
            Note = "Minor Point"
        },
        new FontDto
        {
            Type = FontTypes.TEXT,
            FontFamily = DefaultFontFamily,
            Tag = "P",
            FontWeight = "400",
            FontSize = "14px",
            LineHeight = "20px",
            LetterSpacing = "0px",
            Note = "Paragraph"
        },
        new FontDto
        {
            Type = FontTypes.TEXT,
            FontFamily = DefaultFontFamily,
            Tag = "Button",
            FontWeight = "500",
            FontSize = "14px",
            LineHeight = "20px",
            LetterSpacing = "0px",
            Note = "Button"
        }
    ];
}