﻿using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class BrandingResponse(
        string? id,
        string theme,
        string? websiteUrl,
        IEnumerable<FontDto> fonts,
        List<ColorDto> colors,
        IEnumerable<ResponsiveBreakpointDto> responsiveBreakpoints,
        string faviconUrl,
        string logoUrl,
        List<LogoDto>? logos,
        string defaultFont,
        bool hasCompactButtons,
        string defaultRadius,
        string defaultSpacing,
        LoaderTypes loader,
        FocusRingTypes focusRing,
        CardStyleTypes cardStyle)
    : ISuccess
{
    const string DefaultFontFamily = "Open Sans";

    public string? Id { get; } = id;
    public string Theme { get; } = theme;
    public List<ColorDto> Colors { get; } = colors;
    public IEnumerable<FontDto> Fonts { get; } = fonts;
    public IEnumerable<ResponsiveBreakpointDto> ResponsiveBreakpoints { get; } = responsiveBreakpoints;
    public string FaviconUrl { get; } = faviconUrl;
    public string LogoUrl { get; } = logoUrl;
    public List<LogoDto>? Logos { get; } = logos;
    public string DefaultRadius { get; } = defaultRadius;
    public string DefaultFont { get; } = defaultFont;
    public string DefaultSpacing { get; } = defaultSpacing;
    public string? WebsiteUrl { get; } = websiteUrl;
    public bool HasCompactButtons { get; } = hasCompactButtons;
    public LoaderTypes Loader { get; } = loader;
    public FocusRingTypes FocusRing { get; } = focusRing;
    public CardStyleTypes CardStyle { get; } = cardStyle;

    public static BrandingResponse GetDefault()
    {
        return new BrandingResponse(
            null,
            "light",
            null,
            new List<FontDto>
            {
                new()
                {
                    FontFamily = DefaultFontFamily,
                    Tag = "H1",
                    FontWeight = "700",
                    FontSize = "3.052rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.5px"
                },
                new()
                {
                    FontFamily = DefaultFontFamily,
                    Tag = "H2",
                    FontWeight = "700",
                    FontSize = "2.441rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.4px"
                },
                new()
                {
                    FontFamily = DefaultFontFamily,
                    Tag = "H3",
                    FontWeight = "700",
                    FontSize = "1.953rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.3px"
                },
                new()
                {
                    FontFamily = DefaultFontFamily,
                    Tag = "H4",
                    FontWeight = "700",
                    FontSize = "1.563rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.2px"
                },
                new()
                {
                    FontFamily = DefaultFontFamily,
                    Tag = "H5",
                    FontWeight = "700",
                    FontSize = "1.25rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.1px"
                },
                new()
                {
                    FontFamily = DefaultFontFamily,
                    Tag = "H6",
                    FontWeight = "700",
                    FontSize = "1rem",
                    LineHeight = "1.2",
                    LetterSpacing = "0.1px"
                },
                new()
                {
                    FontFamily = DefaultFontFamily,
                    Tag = "P",
                    FontWeight = "400",
                    FontSize = "0.8rem",
                    LineHeight = "1.5",
                    LetterSpacing = "0.5px"
                }
            },
            new List<ColorDto>
            {
                new() { Hex = "#2F65CB", Name = "Primary", IsDefault = true, FriendlyName = "Primary", },
                new() { Hex = "#FFFFFF", Name = "PrimaryText", IsDefault = true, FriendlyName = "Primary Text", },
                new() { Hex = "#D9D9D9", Name = "Secondary", IsDefault = true, FriendlyName = "Secondary" },
                new() { Hex = "#000000", Name = "SecondaryText", IsDefault = true, FriendlyName = "Secondary Text" },
                new() { Hex = "#E57F4F", Name = "Tertiary", IsDefault = true, FriendlyName = "Tertiary" },
                new() { Hex = "#FFFFFF", Name = "TertiaryText", IsDefault = true, FriendlyName = "Tertiary Text" },
                new() { Hex = "#FF9600", Name = "Background", IsDefault = true, FriendlyName = "Background" },
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
                new() { Breakpoint = "100%", Type = "Desktop" },
                new() { Breakpoint = "768px", Type = "Tablet" },
                new() { Breakpoint = "480px", Type = "Mobile" }
            },
            "",
            "",
            null,
            DefaultFontFamily,
            true,
            "sm",
            "md",
            LoaderTypes.OVAL,
            FocusRingTypes.DEFAULT,
            CardStyleTypes.ROUNDED);
    }

    private static IEnumerable<string> _getDefaultColorNames()
    {
        return new[]
        {
            "Primary", "PrimaryText", "Secondary", "SecondaryText", "Tertiary", "TertiaryText", "Danger", "Warning",
            "Success", "Neutral", "Black",
            "White", "Border"
        };
    }

    public static bool CheckAllDefaultColorsExists(
        IEnumerable<string> colorNames,
        out IEnumerable<string> missingColorNames)
    {
        // Check to make sure all default colors exist
        IEnumerable<string> defaultColorNames = _getDefaultColorNames();
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