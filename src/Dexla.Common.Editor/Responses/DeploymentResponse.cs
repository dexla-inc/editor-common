using Dexla.Common.Editor.Entities;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class DeploymentResponse : ISuccess
{
    public string Id { get; }
    public string ProjectId { get; }
    public EnvironmentTypes Environment { get; }
    public string CommitMessage { get; }
    public string TaskId { get; }
    public string Version { get; }
    public List<DeploymentPage> Pages { get; set; } = [];

    public DeploymentResponse(
        string id, 
        string projectId, 
        EnvironmentTypes environment, 
        string commitMessage,
        string taskId,
        string version)
    {
        Id = id;
        ProjectId = projectId;
        Environment = environment;
        CommitMessage = commitMessage;
        TaskId = taskId;
        Version = version;
    }

    public DeploymentResponse()
    {
        
    } 

    public string TrackingId { get; set; }
}