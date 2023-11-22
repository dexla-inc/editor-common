using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Models;

public class BrandingModel : IModelWithUserId
{
    public string? Id // Need to refactor to support multiple themes
    {
        get => ProjectId;
        set
        {
            if (value != null) ProjectId = value;
        }
    }

    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty; 
    public List<FontDto> Fonts { get; set; } = new();
    public List<ColorDto> Colors { get; set; } = new();
    public List<ResponsiveBreakpointDto> ResponsiveBreakpoints { get; set; } = new();
    public string LogoUrl { get; set; } = string.Empty;
    public List<LogoDto>? Logos { get; set; }
    public string FaviconUrl { get; set; } = string.Empty;
    public string DefaultSpacing { get; set; } = string.Empty;
    public string? WebsiteUrl { get; set; }
    public string DefaultRadius { get; set; } = string.Empty;
    public string DefaultFont { get; set; } = string.Empty;
    public bool HasCompactButtons { get; set; }
    public string Loader { get; set; } = string.Empty;
    public string FocusRing { get; set; } = string.Empty;
    public string CardStyle { get; set; } = string.Empty;

    public void SetUserId(string value)
    {
        UserId = value;
    }

    public void SetProjectId(string projectId)
    {
        ProjectId = projectId;
    }

    public void CleanWebsiteUrl()
    {
        WebsiteUrl = WebsiteUrl?.TrimEnd('/');
    }

    const string DefaultFontFamily = "Open Sans";

    public static BrandingModel GetDefault(string userId, string projectId)
    {
        return new BrandingModel
        {
            UserId = userId,
            ProjectId = projectId,
            Fonts = new List<FontDto>
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
            Colors = new List<ColorDto>
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
            ResponsiveBreakpoints = new List<ResponsiveBreakpointDto>
            {
                new() { Breakpoint = "100%", Type = "Desktop" },
                new() { Breakpoint = "768px", Type = "Tablet" },
                new() { Breakpoint = "480px", Type = "Mobile" }
            },
            DefaultFont = DefaultFontFamily,
            HasCompactButtons = true,
            DefaultRadius = "sm",
            DefaultSpacing = "md",
            Loader = LoaderTypes.OVAL.ToString(),
            FocusRing = FocusRingTypes.DEFAULT.ToString(),
            CardStyle = CardStyleTypes.ROUNDED.ToString(),
            FaviconUrl = "https://mortenjonassen.dk/wp-content/uploads/2020/02/Per-larsen-favicon.jpg",
            LogoUrl = "https://wwwspennarecom.cdn.triggerfish.cloud/uploads/2015/08/Your-Logo-Here-Black-22.jpg",
        };
    }
}