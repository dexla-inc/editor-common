using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class ProjectModel : IModelWithUserId
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string FriendlyName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? SimilarCompany { get; set; }
    public List<ProjectCollaboratorDto> Collaborators { get; set; } = [];
    public string Domain { get; set; } = string.Empty;
    public string SubDomain { get; set; } = string.Empty;
    public long Created { get; set; }
    public string[] Screenshots { get; set; } = Array.Empty<string>();
    public bool IsOwner { get; set; }
    public string? CustomCode { get; set; }
    public string? RedirectSlug { get; set; }
    public string? FaviconUrl { get; set; }
    
    public void SetUserId(string value)
    {
        UserId = value;
    }

    public void SetName(string value)
    {
        Name = value;
    }

    public void SetFriendlyName(string value)
    {
        FriendlyName = value;
    }

    public void SetCreated()
    {
        Created = DateTimeExtensions.GetTimestamp();
    }

    public void SetCompanyId(string value)
    {
        CompanyId = value;
    }
    
    public void SetOwner(bool value)
    {
        IsOwner = value;
    }
}