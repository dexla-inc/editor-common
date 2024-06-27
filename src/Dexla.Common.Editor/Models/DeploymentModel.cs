using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Models;

public class DeploymentModel : IModelWithProjectId
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
    public string CommitMessage { get; set; } = string.Empty;
    public string TaskId { get; set; } = string.Empty;
    public EnvironmentTypes Environment { get; set; }
    public int Version { get; set; }
    public ProjectModel? Project { get; set; }
    public BrandingModel? Branding { get; set; }
    
    public DeploymentModel CloneWithEnvironment(EnvironmentTypes environment)
    {
        return new DeploymentModel
        {
            Id = Id,
            EntityStatus = EntityStatus,
            UserId = UserId,
            CompanyId = CompanyId,
            ProjectId = ProjectId,
            CommitMessage = CommitMessage,
            TaskId = TaskId,
            Environment = environment,
            Version = Version,
            Project = Project,
            Branding = Branding
        };
    }

    public void SetUserId(string value)
    {
        UserId = value;
    }

    public void SetProjectId(string projectId)
    {
        ProjectId = projectId;
    }

    public void SetEnvironment(bool forceProduction)
    {
        Environment = forceProduction ? EnvironmentTypes.Production : EnvironmentTypes.Staging;
    }

    public void SetProject(ProjectModel project)
    {
        Project = project;
    }

    public void SetBranding(BrandingModel branding)
    {
        Branding = branding;
    }
}