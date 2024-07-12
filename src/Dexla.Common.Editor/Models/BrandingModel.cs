using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Models;

public class BrandingModel : IModelWithProjectId
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
    public BasicAuditInformation? AuditInformation { get; set; }

    public void SetCompanyId(string value)
    {
        CompanyId = value;
    }

    public string UserId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public List<FontDto> Fonts { get; set; } = [];
    public List<ColorDto> Colors { get; set; } = [];
    public List<ColorShadeDto> ColorShades { get; set; } = [];
    public List<ResponsiveBreakpointDto> ResponsiveBreakpoints { get; set; } = [];
    public string LogoUrl { get; set; } = string.Empty;
    public List<LogoDto>? Logos { get; set; }
    public string FaviconUrl { get; set; } = string.Empty;
    public string DefaultSpacing { get; set; } = string.Empty;
    public string? WebsiteUrl { get; set; }
    public string DefaultRadius { get; set; } = string.Empty;
    public string DefaultFont { get; set; } = string.Empty;
    public string InputSize { get; set; } = string.Empty;
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
    
    public void SetIds(string userId, string projectId)
    {
        UserId = userId;
        ProjectId = projectId;
        Id = projectId;
    }

    const string DefaultFontFamily = "Open Sans";

    public static BrandingModel GetDefault(string userId, string projectId)
    {
        return new BrandingModel
        {
            UserId = userId,
            ProjectId = projectId,
            Fonts =
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
            ],
            ColorShades = Models.ColorShades.Colors,
            Colors =
            [
                new ColorDto { Hex = "#2F65CB", Name = "Primary", IsDefault = true, FriendlyName = "Primary", },
                new ColorDto
                    { Hex = "#FFFFFF", Name = "PrimaryText", IsDefault = true, FriendlyName = "Primary Text", },
                new ColorDto { Hex = "#D9D9D9", Name = "Secondary", IsDefault = true, FriendlyName = "Secondary" },
                new ColorDto
                    { Hex = "#000000", Name = "SecondaryText", IsDefault = true, FriendlyName = "Secondary Text" },
                new ColorDto { Hex = "#E57F4F", Name = "Tertiary", IsDefault = true, FriendlyName = "Tertiary" },
                new ColorDto
                    { Hex = "#FFFFFF", Name = "TertiaryText", IsDefault = true, FriendlyName = "Tertiary Text" },
                new ColorDto { Hex = "#FFFFFF", Name = "Background", IsDefault = true, FriendlyName = "Background" },
                new ColorDto { Hex = "#FE191C", Name = "Danger", IsDefault = true, FriendlyName = "Danger" },
                new ColorDto { Hex = "#FFCC00", Name = "Warning", IsDefault = true, FriendlyName = "Warning" },
                new ColorDto { Hex = "#10D48E", Name = "Success", IsDefault = true, FriendlyName = "Success" },
                new ColorDto { Hex = "#F1F1F1", Name = "Neutral", IsDefault = true, FriendlyName = "Neutral" },
                new ColorDto { Hex = "#000000", Name = "Black", IsDefault = true, FriendlyName = "Black" },
                new ColorDto { Hex = "#FFFFFF", Name = "White", IsDefault = true, FriendlyName = "White" },
                new ColorDto { Hex = "#EEEEEE", Name = "Border", IsDefault = true, FriendlyName = "Border" }
            ],
            ResponsiveBreakpoints =
            [
                new ResponsiveBreakpointDto { Breakpoint = "100%", Type = "Desktop" },
                new ResponsiveBreakpointDto { Breakpoint = "768px", Type = "Tablet" },
                new ResponsiveBreakpointDto { Breakpoint = "480px", Type = "Mobile" }
            ],
            DefaultFont = DefaultFontFamily,
            HasCompactButtons = true,
            DefaultRadius = "sm",
            DefaultSpacing = "md",
            InputSize = "sm",
            Loader = LoaderTypes.OVAL.ToString(),
            FocusRing = FocusRingTypes.DEFAULT.ToString(),
            CardStyle = CardStyleTypes.ROUNDED.ToString(),
            FaviconUrl = "https://mortenjonassen.dk/wp-content/uploads/2020/02/Per-larsen-favicon.jpg",
            LogoUrl = "https://wwwspennarecom.cdn.triggerfish.cloud/uploads/2015/08/Your-Logo-Here-Black-22.jpg",
        };
    }
}