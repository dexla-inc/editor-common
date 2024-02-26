using System.Collections.Generic;
using Dexla.Common.Editor.Entities;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class DeploymentModel : IModelWithUserId
{
    public string? Id { get; set; } = UtilityExtensions.GetId();
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string CommitMessage { get; set; } = string.Empty;
    public string TaskId { get; set; } = string.Empty;
    public EnvironmentTypes Environment { get; set; }
    public int Version { get; set; }
    public List<DeploymentPage> Pages { get; set; } = [];

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

    public void AddPages(List<DeploymentPage> pages)
    {
        Pages = pages;
    }
}