using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Entities;

public class Theme : IEntity
{
    protected Theme()
    {
    }

    public string Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; }
    public string ProjectId { get; set; } = string.Empty;
    public List<Font> Fonts { get; set; }
    public List<Color> Colors { get; set; }
    public List<Logo>? Logos { get; set;  } 
    public List<ResponsiveBreakpoint> ResponsiveBreakpoints { get; set; }
    [Obsolete("Use Logos instead")]
    public string LogoUrl { get; set; } = string.Empty;
    public string FaviconUrl { get; set; } = string.Empty;
    public string DefaultBorderRadius { get; set; }
    public string DefaultSpacing { get; set; } 
    public string? WebsiteUrl { get; set; }
    public string DefaultRadius { get; set; } = string.Empty;
    public string DefaultFont { get; set; } = string.Empty;
    public bool HasCompactButtons { get; set; }
    public LoaderTypes Loader { get; set; }
    public FocusRingTypes FocusRing { get; set; }
}