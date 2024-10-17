using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class BrandingResponse(
    string? id,
    Contrasts theme,
    string? websiteUrl,
    IEnumerable<FontDto> fonts,
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
    public Contrasts Theme { get; } = theme;
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
        return
        [
            "Primary",
            "PrimaryText", 
            "Secondary", 
            "SecondaryText", 
            "Tertiary", 
            "TertiaryText", 
            "Danger", 
            "Warning",
            "Success", 
            "Neutral", 
            "Black",
            "White", 
            "Border"
        ];
    }
    
    private static IEnumerable<string> _getDefaultColorShadeNames()
    {
        return
        [
            "Primary.6",
            "PrimaryText.6", 
            "Secondary.6", 
            "SecondaryText.6", 
            "Tertiary.6", 
            "TertiaryText.6", 
            "Danger.6", 
            "Warning.6",
            "Success.6", 
            "Neutral.6", 
            "Black.6",
            "White.6", 
            "Border.6"
        ];
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

    public static bool CheckAllDefaultColorShadesExist(
        IEnumerable<string> colorNames,
        out IEnumerable<string> missingColorNames)
    {
        // Check to make sure all default colors exist
        IEnumerable<string> defaultColorNames = _getDefaultColorShadeNames();
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
        bool isThemeValid = Enum.TryParse(model.Theme, out Contrasts theme);
        
        return new BrandingResponse(
            model.Id,
            isThemeValid ? theme : Contrasts.LIGHT,
            model.WebsiteUrl,
            model.Fonts,
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
    
    public static IResponse ModelToResponse(RepositoryActionResultModel<BrandingModel> actionResult)
    {
        return actionResult.ActionResult(
            actionResult,
            ModelToResponse);
    }

    public static BrandingResponse EntityToResponse(Branding entity)
    {
        bool isThemeValid = Enum.TryParse(entity.Theme, out Contrasts theme);
        
        return new BrandingResponse(
            entity.Id,
            isThemeValid ? theme : Contrasts.LIGHT,
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