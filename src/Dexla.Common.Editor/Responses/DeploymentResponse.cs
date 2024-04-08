using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
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
    public List<DeploymentPageResponse> Pages { get; private set; } = [];

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

    public void SetPages(IEnumerable<DeploymentPage> pages)
    {
        Pages = pages.Select(p => new DeploymentPageResponse(
            p.Id,
            p.Title,
            p.Slug,
            p.AuthenticatedOnly,
            p.AuthenticatedUserRole,
            p.PageState,
            p.Actions?.Select(a => new PageActionDto
            {
                Id = a.Id,
                Trigger = a.Trigger,
                Action = Json.Deserialize<object>(a.Action),
                SequentialTo = a.SequentialTo
            }).ToList())
        ).ToList();
    }

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
    
    public static DeploymentResponse EntityToResponse(DeploymentWithPages entity)
    {
        return new DeploymentResponse(
            entity.Id,
            entity.ProjectId,
            entity.Environment,
            entity.CommitMessage,
            entity.TaskId,
            entity.Version)
        {
            Pages = entity.Pages.Select(p => new DeploymentPageResponse(
                p.Id,
                p.Title,
                p.Slug,
                p.AuthenticatedOnly,
                p.AuthenticatedUserRole,
                p.PageState,
                p.Actions?.Select(a => new PageActionDto
                {
                    Id = a.Id,
                    Trigger = a.Trigger,
                    Action = Json.Deserialize<object>(a.Action),
                    SequentialTo = a.SequentialTo
                }).ToList())
            ).ToList()
        };
    }

    public static DeploymentResponse EntityToResponse(Deployment model) => EntityToResponse()(model);

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

    public static IResponse ModelToResponse(
        RepositoryActionResultModel<DeploymentModel> actionResult)
    {
        return actionResult.ActionResult(
            actionResult,
            ModelToResponse());
    }
}