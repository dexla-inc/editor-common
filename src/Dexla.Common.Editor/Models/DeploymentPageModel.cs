using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;

namespace Dexla.Common.Editor.Models;

public class DeploymentPageModel : IModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }
    public string ProjectId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string DeploymentId { get; set; } = string.Empty;
    public string PageId { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool AuthenticatedOnly { get; set; }
    public string AuthenticatedUserRole { get; set; } = string.Empty;
    public List<string> PageState { get; set; } = [];
    public ProjectModel Project { get; set; } = new();
    public BrandingModel Branding { get; set; } = new();

    public DeploymentPageModel Clone()
    {
        return new DeploymentPageModel
        {
            Id = Id,
            EntityStatus = EntityStatus,
            ProjectId = ProjectId,
            UserId = UserId,
            DeploymentId = DeploymentId,
            PageId = PageId,
            Environment = Environment,
            Title = Title,
            Slug = Slug,
            Description = Description,
            AuthenticatedOnly = AuthenticatedOnly,
            AuthenticatedUserRole = AuthenticatedUserRole,
            PageState = PageState,
            Project = Project,
            Branding = Branding
        };
    }
}