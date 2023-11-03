using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Models;

public class ThemeModel : IModelWithUserId
{
    public string? Id
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
}