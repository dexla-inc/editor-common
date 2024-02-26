using System.Collections.Generic;
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
    public int Version { get; }
    public List<DeploymentPageResponse> Pages { get; }

    public DeploymentResponse(
        string id, 
        string projectId, 
        EnvironmentTypes environment, 
        string commitMessage,
        string taskId,
        int version, 
        List<DeploymentPageResponse> pages)
    {
        Id = id;
        ProjectId = projectId;
        Environment = environment;
        CommitMessage = commitMessage;
        TaskId = taskId;
        Version = version;
        Pages = pages;
    }

    public DeploymentResponse()
    {
        
    } 

    public string TrackingId { get; set; }
}