using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Entities;

public class Project : IEntity
{
    protected internal Project()
    {
    }

    public string Id { get; set; } = UtilityExtensions.GetId();
    public EntityStatus EntityStatus { get; set; }
    public virtual string UserId { get; set; }
    public virtual string CompanyId { get; set; }
    public virtual string FriendlyName { get; set; }
    public virtual string Name { get; set; }
    public virtual RegionTypes Region { get; set; }
    public virtual ProjectTypes Type { get; set; }
    public virtual string Industry { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual string? SimilarCompany { get; set; }
    public virtual List<ProjectCollaborator> Collaborators { get; set; } = [];
    public string Domain { get; set; } = string.Empty;
    public string SubDomain { get; set; } = string.Empty;
    public long Created { get; set; }
    public string[] Screenshots { get; set; }
    public bool IsOwner { get; set; }
    public string? CustomCode { get; set; }
    [Obsolete("Use RedirectSlug instead.")]
    public string? RedirectSlug { get; set; }
    public string? FaviconUrl { get; set; }
    public Redirects Redirects { get; set; } = new();
    public Dictionary<string, object> Metadata { get; set; } = new();
    public List<App>? Apps { get; set; } 
}
