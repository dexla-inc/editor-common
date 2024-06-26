using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Entities;

public class DeploymentPage : IEntity
{
    public string Id { get; set; } = string.Empty;
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string DeploymentId { get; set; } = string.Empty;
    public string PageId { get; set; } = string.Empty;
    public EnvironmentTypes Environment { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public bool AuthenticatedOnly { get; set; }
    public string AuthenticatedUserRole { get; set; } = string.Empty;
    public List<string> PageState { get; set; } = [];
    public virtual List<PageAction>? Actions { get; set; }
    public Project? Project { get; set; }
    public Branding? Branding { get; set; }
    
    public DeploymentPage Clone()
    {
        return new DeploymentPage
        {
            Id = Id,
            EntityStatus = EntityStatus,
            UserId = UserId,
            ProjectId = ProjectId,
            DeploymentId = DeploymentId,
            PageId = PageId,
            Environment = Environment,
            Title = Title,
            Slug = Slug,
            AuthenticatedOnly = AuthenticatedOnly,
            AuthenticatedUserRole = AuthenticatedUserRole,
            PageState = PageState,
            Actions = Actions,
            Project = Project,
            Branding = Branding
        };
    }
}