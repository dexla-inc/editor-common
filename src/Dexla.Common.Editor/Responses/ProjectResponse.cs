using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class ProjectResponse : ISuccess
{
    public string Id { get; }
    public string Name { get; }
    public string FriendlyName { get; }
    public Region Region { get; }
    public ProjectTypes Type { get; }
    public string Industry { get; }
    public string Description { get; }
    public string? SimilarCompany { get; }
    public UserRoles AccessLevel { get; }
    public bool IsOwner { get; }
    public string Domain { get; }
    public string SubDomain { get; }
    public List<ProjectCollaboratorDto> Collaborators { get; }
    public long Created { get; }
    public string[] Screenshots { get; }
    
    public ProjectResponse(
        string id,
        string name,
        string friendlyName,
        Region region,
        ProjectTypes type,
        string industry,
        string description,
        string? similarCompany,
        UserRoles accessLevel,
        string domain,
        string subDomain,
        List<ProjectCollaboratorDto> collaborators,
        long created,
        string[] screenshots)
    {
        Id = id;
        Name = name;
        FriendlyName = friendlyName;
        Region = region;
        Type = type;
        Industry = industry;
        Description = description;
        SimilarCompany = similarCompany;
        AccessLevel = accessLevel;
        IsOwner = accessLevel == UserRoles.OWNER;
        Domain = domain;
        SubDomain = subDomain;
        Collaborators = collaborators;
        Created = created;
        Screenshots = screenshots;
    }

    public ProjectResponse()
    {
        
    }

    public string TrackingId { get; set; }
}