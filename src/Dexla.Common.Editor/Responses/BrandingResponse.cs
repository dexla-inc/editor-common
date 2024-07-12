using Dexla.Common.Editor.Entities;
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
        List<ColorShadeDto> colorShades,
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
    public List<ColorShadeDto> ColorShades { get; } = colorShades;
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

    public static BrandingResponse ModelToResponse(BrandingModel model)
    {
        return new BrandingResponse(
            model.Id,
            model.Theme,
            model.WebsiteUrl,
            model.Fonts,
            model.Colors,
            model.ColorShades,
            model.ResponsiveBreakpoints,
            model.FaviconUrl,
            model.LogoUrl,
            model.Logos,
            model.DefaultFont,
            model.HasCompactButtons,
            model.DefaultRadius,
            model.DefaultSpacing,
            model.InputSize,
            Enum.Parse<LoaderTypes>(model.Loader),
            Enum.Parse<FocusRingTypes>(model.FocusRing),
            Enum.Parse<CardStyleTypes>(model.CardStyle)
        );
    }
    
    public static BrandingResponse EntityToResponse(Branding entity)
    {
        return new BrandingResponse(
            entity.Id,
            entity.Theme,
            entity.WebsiteUrl,
            entity.Fonts.Select(f => new FontDto
            {
                Type = f.Type,
                FontFamily = f.FontFamily,
                Tag = f.Tag,
                FontWeight = f.FontWeight,
                FontSize = f.FontSize,
                LineHeight = f.LineHeight,
                LetterSpacing = f.LetterSpacing,
                Note = f.Note
            }),
            entity.Colors.Select(c => new ColorDto
            {
                Name = c.Name,
                FriendlyName = c.FriendlyName,
                Hex = c.Hex,
                Brightness = c.Brightness,
                IsDefault = c.IsDefault
            }).ToList(),
            entity.ColorShades.Select(c => new ColorShadeDto
            {
                Name = c.Name,
                FriendlyName = c.FriendlyName,
                Hex = c.Hex,
                IsDefault = c.IsDefault
            }).ToList(),
            entity.ResponsiveBreakpoints.Select(rb => new ResponsiveBreakpointDto
            {
                Breakpoint = rb.Breakpoint,
                Type = rb.Type.ToString()
            }),
            entity.FaviconUrl,
            entity.LogoUrl,
            entity.Logos?.Select(l => new LogoDto
            {
                Url = l.Url,
                Type = l.Type.ToString()
            }).ToList(),
            entity.DefaultFont,
            entity.HasCompactButtons,
            entity.DefaultRadius,
            entity.DefaultSpacing,
            entity.InputSize,
            entity.Loader,
            entity.FocusRing,
            entity.CardStyle
        );
    }
}