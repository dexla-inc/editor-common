using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class ThemeResponse : ISuccess
{
    public string? Id { get; }

    public List<ColorDto> Colors { get; }

    public IEnumerable<FontDto> Fonts { get; }
    public IEnumerable<ResponsiveBreakpointDto> ResponsiveBreakpoints { get; }
    public string FaviconUrl { get; }
    public string LogoUrl { get; }
    public List<LogoDto>? Logos { get; } 
    public string DefaultBorderRadius { get; }
    public string DefaultSpacing { get; }
    public string? WebsiteUrl { get; }

    public ThemeResponse(
        string? id,
        IEnumerable<FontDto> fonts,
        List<ColorDto> colors,
        IEnumerable<ResponsiveBreakpointDto> responsiveBreakpoints, 
        string faviconUrl, 
        string logoUrl, 
        List<LogoDto>? logos,
        string defaultBorderRadius,
        string defaultSpacing,
        string? websiteUrl)
    {
        Id = id;
        Fonts = fonts;
        Colors = colors;
        ResponsiveBreakpoints = responsiveBreakpoints;
        FaviconUrl = faviconUrl;
        LogoUrl = logoUrl;
        Logos = logos;
        DefaultBorderRadius = defaultBorderRadius;
        DefaultSpacing = defaultSpacing;
        WebsiteUrl = websiteUrl;
    }

    public static ThemeResponse GetDefault()
    {
        return new ThemeResponse(
            null,
            new List<FontDto>
            {
                new()
                {
                    FontFamily = "Open Sans",
                    Tag = "H1",
                    FontWeight = "700",
                    FontSize = "3.052rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.5px"
                },
                new()
                {
                    FontFamily = "Open Sans",
                    Tag = "H2",
                    FontWeight = "700",
                    FontSize = "2.441rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.4px"
                },
                new()
                {
                    FontFamily = "Open Sans",
                    Tag = "H3",
                    FontWeight = "700",
                    FontSize = "1.953rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.3px"
                },
                new()
                {
                    FontFamily = "Open Sans",
                    Tag = "H4",
                    FontWeight = "700",
                    FontSize = "1.563rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.2px"
                },
                new()
                {
                    FontFamily = "Open Sans",
                    Tag = "H5",
                    FontWeight = "700",
                    FontSize = "1.25rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.1px"
                },
                new()
                {
                    FontFamily = "Open Sans",
                    Tag = "H6",
                    FontWeight = "700",
                    FontSize = "1rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.1px"
                },
                new()
                {
                    FontFamily = "Open Sans",
                    Tag = "P",
                    FontWeight = "400",
                    FontSize = "0.8rem",
                    LineHeight = "1.5",
                    LetterSpacing = "0.5px"
                }
            },
            new List<ColorDto>
            {
                new() { Hex = "#0069FF", Name = "Primary",  IsDefault = true, FriendlyName = "Primary", },
                new() { Hex = "#FF9600", Name = "Accent", IsDefault = true, FriendlyName = "Accent" },
                new() { Hex = "#FE191C", Name = "Danger", IsDefault = true, FriendlyName = "Danger" },
                new() { Hex = "#FFCC00", Name = "Warning", IsDefault = true, FriendlyName = "Warning" },
                new() { Hex = "#10D48E", Name = "Success", IsDefault = true, FriendlyName = "Success" },
                new() { Hex = "#F1F1F1", Name = "Neutral", IsDefault = true, FriendlyName = "Neutral" },
                new() { Hex = "#000000", Name = "Black", IsDefault = true, FriendlyName = "Black" },
                new() { Hex = "#FFFFFF", Name = "White", IsDefault = true, FriendlyName = "White" },
                new() { Hex = "#EEEEEE", Name = "Border", IsDefault = true, FriendlyName = "Border" }
            },
            new List<ResponsiveBreakpointDto>
            {
                new() { Breakpoint = "100%", Type = "Desktop"},
                new() { Breakpoint = "768px", Type = "Tablet" },
                new() { Breakpoint = "480px", Type = "Mobile" }
            },
            "",
            "",
            null,
            "sm",
            "md",
            null);
    }

    public static string[] GetDefaultColorNames()
    {
        return new []{ "Primary", "Accent", "Danger", "Warning", "Success", "Neutral", "Black", "White", "Border" };
    }
    
    public static bool CheckAllDefaultColorsExists(IEnumerable<string> colorNames, out IEnumerable<string> missingColorNames)
    {
        // Check to make sure all default colors exist
        string[] defaultColorNames = GetDefaultColorNames();
        List<string> missingColors = defaultColorNames.Where(colorName => !colorNames.Contains(colorName)).ToList();
        if (missingColors.Any())
        {
            missingColorNames = missingColors;
            return false;
        }

        missingColorNames = new List<string>();
        return true;
    }

    public string TrackingId { get; set; }
}