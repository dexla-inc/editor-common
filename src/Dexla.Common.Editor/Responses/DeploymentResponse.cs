﻿using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
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
    //public List<DeploymentPageResponse> Pages { get; }

    public DeploymentResponse(
        string id,
        string projectId,
        EnvironmentTypes environment,
        string commitMessage,
        string taskId,
        int version)
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

    public static Func<Deployment, DeploymentResponse> EntityToResponse()
    {
        return entity => new DeploymentResponse(
            entity.Id,
            entity.ProjectId,
            entity.Environment,
            entity.CommitMessage,
            entity.TaskId,
            entity.Version);
    }

    public static DeploymentResponse EntityToResponse(Deployment model)
    {
        return EntityToResponse()(model);
    }
    
    public static Func<DeploymentModel, DeploymentResponse> ModelToResponse()
    {
        return model => new DeploymentResponse(
            model.Id!,
            model.ProjectId,
            model.Environment,
            model.CommitMessage,
            model.TaskId,
            model.Version);
    }
}