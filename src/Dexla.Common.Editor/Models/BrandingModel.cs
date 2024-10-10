using System.Text.RegularExpressions;
using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Models;

public class BrandingModel : IModelWithProjectId, IModelWithOnboarding
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
    
    public static BrandingModel GetDefault(string userId, string projectId)
    {
        return new BrandingModel
        {
            Theme = Contrasts.LIGHT.ToString(),
            UserId = userId,
            ProjectId = projectId,
            Fonts = Models.Fonts.DefaultFonts,
            ColorShades = Models.ColorShades.DefaultShades,
            Colors = Models.ColorShades.DefaultColors,
            ResponsiveBreakpoints =
            [
                new ResponsiveBreakpointDto { Breakpoint = "100%", Type = "Desktop" },
                new ResponsiveBreakpointDto { Breakpoint = "768px", Type = "Tablet" },
                new ResponsiveBreakpointDto { Breakpoint = "480px", Type = "Mobile" }
            ],
            DefaultFont = Models.Fonts.DefaultFontFamily,
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

    public bool IsOnboarding { get; set; }

    public void SetOnboarding(bool value)
    {
        IsOnboarding = value;
    }
    
    /// <summary>
    /// Sets a new color for the specified type by updating the Colors and ColorShades lists.
    /// </summary>
    /// <param name="type">The color type (e.g., "Primary", "Secondary").</param>
    /// <param name="newHex">The new hex color code (e.g., "#FF5733").</param>
    ///  /// <param name="isDefault">Is this a default color or new</param>
    public void SetColor(string type, string newHex, bool isDefault = true)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Color type cannot be null or empty.", nameof(type));

        if (string.IsNullOrWhiteSpace(newHex))
            throw new ArgumentException("Hex value cannot be null or empty.", nameof(newHex));

        // Validate hex format
        if (!Regex.IsMatch(newHex, "^#([A-Fa-f0-9]{6})$"))
            throw new ArgumentException("Invalid hex color format.", nameof(newHex));

        // Update the base color in Colors list
        ColorDto? baseColor = Colors.FirstOrDefault(c => c.Name.Equals(type, StringComparison.OrdinalIgnoreCase));
        if (baseColor != null)
        {
            baseColor.Hex = newHex;
        }
        else
        {
            // If the base color does not exist, optionally add it
            Colors.Add(new ColorDto
            {
                Hex = newHex,
                Name = type,
                IsDefault = isDefault,
                FriendlyName = type
            });
        }

        // Update the ColorShades
        ColorShades.SetColorShade(type, newHex, isDefault);
    }
}