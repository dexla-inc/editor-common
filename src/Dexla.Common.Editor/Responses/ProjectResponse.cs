using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class ProjectResponse : ISuccess
{
    public string Id { get; }
    public string CompanyId { get; }
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
    public long Created { get; }
    public string[] Screenshots { get; }
    public string? HomePageId { get; set; }
    
    public ProjectResponse(
        string id,
        string companyId,
        string name,
        string friendlyName,
        Region region,
        ProjectTypes type,
        string industry,
        string description,
        string? similarCompany,
        bool isOwner,
        string domain,
        string subDomain,
        long created,
        string[] screenshots)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        FriendlyName = friendlyName;
        Region = region;
        Type = type;
        Industry = industry;
        Description = description;
        SimilarCompany = similarCompany;
        IsOwner = isOwner;
        Domain = domain;
        SubDomain = subDomain;
        Created = created;
        Screenshots = screenshots;
    }

    public ProjectResponse()
    {
        
    }

    public string TrackingId { get; set; }
}