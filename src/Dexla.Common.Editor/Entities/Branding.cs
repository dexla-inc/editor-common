using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Entities;

public class Branding : IEntity, IEntityWithOnboarding
{
    protected internal Branding()
    {
    }

    public string Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }
    public string CompanyId { get; set; } = string.Empty;
    public string UserId { get; set; }
    public string ProjectId { get; set; } = string.Empty;
    public Contrasts Theme { get; set; } = Contrasts.LIGHT;
    public List<Font> Fonts { get; set; }
    public List<Color> Colors { get; set; } = [];
    public List<ColorShade> ColorShades { get; set; } = [];
    public List<Logo>? Logos { get; set;  } 
    public List<ResponsiveBreakpoint> ResponsiveBreakpoints { get; set; }
    [Obsolete("Use Logos instead")]
    public string LogoUrl { get; set; } = string.Empty;
    public string FaviconUrl { get; set; } = string.Empty;
    public string DefaultSpacing { get; set; } 
    public string? WebsiteUrl { get; set; }
    public string DefaultRadius { get; set; } = string.Empty;
    public string DefaultFont { get; set; } = string.Empty;
    public string InputSize { get; set; } = string.Empty;
    public bool HasCompactButtons { get; set; }
    public LoaderTypes Loader { get; set; }
    public FocusRingTypes FocusRing { get; set; }
    public CardStyleTypes CardStyle { get; set; }
    public bool IsOnboarding { get; set; }
}