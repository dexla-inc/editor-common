using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Models;

public class PageModel : IModelWithProjectId, IModelWithOnboarding
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }

    public void SetCompanyId(string value)
    {
        CompanyId = value;
    }

    public string UserId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsHome { get; set; }
    public bool AuthenticatedOnly { get; set; }
    public string AuthenticatedUserRole { get; set; } = string.Empty;
    public string? ParentPageId { get; set; }
    public bool HasNavigation { get; set; }
    public WebAppType? CopyFrom { get; set; } = new();
    public Dictionary<string,string>? QueryStrings { get; set; }
    public List<PageActionDto>? Actions { get; set; }
    public List<string>? Features { get; set; }
    public string CssType { get; set; }

    public void SetProjectId(string projectId)
    {
        ProjectId = projectId;
    }
    public void SetUserId(string value)
    {
        UserId = value;
    }
    
    public static PageModel Empty(string userId, string projectId, string cssType)
    {
        return new PageModel
        {
            UserId = userId,
            ProjectId = projectId,
            Title = "Home",
            Slug = "/",
            Description = "Home page",
            IsHome = true,
            AuthenticatedOnly = false,
            AuthenticatedUserRole = string.Empty,
            HasNavigation = false,
            CopyFrom = null,
            QueryStrings = null,
            CssType = cssType
        };
    }

    public bool IsOnboarding { get; set; }
    
    public void SetOnboarding(bool value)
    {
        IsOnboarding = value;
    }
}