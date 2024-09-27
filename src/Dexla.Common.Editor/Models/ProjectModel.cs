using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class ProjectModel : IModelWithUserId, IModelWithOnboarding
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string FriendlyName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? SimilarCompany { get; set; }
    [Obsolete("Use LiveUrls instead")]
    public string Domain { get; set; } = string.Empty;
    [Obsolete("Use LiveUrls instead")]
    public string SubDomain { get; set; } = string.Empty;
    public Dictionary<string, LiveUrlDto> LiveUrls { get; set; } = [];
    public long Created { get; set; }
    public string[] Screenshots { get; set; } = [];
    public bool IsOwner { get; set; }
    public string? CustomCode { get; set; }
    public string? FaviconUrl { get; set; }
    public RedirectsDto Redirects { get; set; } = new();
    public Dictionary<string, object> Metadata { get; set; } = new();
    public List<AppDto>? Apps { get; set; }
    public bool IsOnboarding { get; set; }
    
    public void SetCompanyId(string value)
    {
        CompanyId = value;
    }
    
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
    
    public void AddMetadata(string key, object value)
    {
        Metadata.Add(key, value);
    }

    public void SetCreated()
    {
        Created = DateTimeExtensions.GetTimestamp();
    }

    public void SetOwner(bool value)
    {
        IsOwner = value;
    }
    
    public void SetOnboarding(bool value)
    {
        IsOnboarding = value;
    }
}