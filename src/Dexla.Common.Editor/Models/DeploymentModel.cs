using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Models;

public class DeploymentModel : IModelWithUserId
{
    public string? Id { get; set; } 
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string CommitMessage { get; set; } = string.Empty;
    public string TaskId { get; set; } = string.Empty;
    public EnvironmentTypes Environment { get; set; }
    public int Version { get; set; }
    public ProjectModel Project { get; set; } = new();
    public BrandingModel Branding { get; set; } = new();

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