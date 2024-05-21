using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class ComponentModel : IModelWithProjectId
{
    public string? Id { get; set; } = UtilityExtensions.GetId();
    public EntityStatus EntityStatus { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImagePreviewUrl { get; set; }
    public string UserRole { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;

    public void SetUserId(string value)
    {
        UserId = value;
    }

    public void SetProjectId(string projectId)
    {
        ProjectId = projectId;
    }

    public void SetCompanyId(string value)
    {
        CompanyId = value;
    }
    public void SetUserRole(string userRole)
    {
        UserRole = userRole;
    }
}