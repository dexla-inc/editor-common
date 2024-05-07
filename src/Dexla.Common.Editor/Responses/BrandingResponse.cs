using System.Collections.Generic;
using System.Linq;
using Dexla.Common.Editor.Models;
using Dexla.Common.Types.Enums;
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
        string inputSize,
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
    public string InputSize { get; } = inputSize;
    public string? WebsiteUrl { get; } = websiteUrl;
    public bool HasCompactButtons { get; } = hasCompactButtons;
    public LoaderTypes Loader { get; } = loader;
    public FocusRingTypes FocusRing { get; } = focusRing;
    public CardStyleTypes CardStyle { get; } = cardStyle;

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

    public static BrandingResponse ModelToResponse(BrandingModel entityBranding)
    {
        return new BrandingResponse(
            entityBranding.Id,
            entityBranding.Theme,
            entityBranding.WebsiteUrl,
            entityBranding.Fonts,
            entityBranding.Colors,
            entityBranding.ResponsiveBreakpoints,
            entityBranding.FaviconUrl,
            entityBranding.LogoUrl,
            entityBranding.Logos,
            entityBranding.DefaultFont,
            entityBranding.HasCompactButtons,
            entityBranding.DefaultRadius,
            entityBranding.DefaultSpacing,
            entityBranding.InputSize,
            Enum.Parse<LoaderTypes>(entityBranding.Loader),
            Enum.Parse<FocusRingTypes>(entityBranding.FocusRing),
            Enum.Parse<CardStyleTypes>(entityBranding.CardStyle)
        );
    }
}