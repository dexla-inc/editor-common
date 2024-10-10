using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class DeploymentResponse : ISuccess, IAuditInformation
{
    public string Id { get; }
    public string ProjectId { get; }
    public EnvironmentTypes Environment { get; }
    public string CommitMessage { get; }
    public string TaskId { get; }
    public int Version { get; }
    public bool CanPromote { get; }
    public List<DeploymentPageResponse> Pages { get; private set; } = [];
    public AuditUserDto UpdatedBy { get; }
    public ProjectResponse? Project { get; }
    public BrandingResponse? Branding { get; }
    public List<ApiIncludeResponse>? Apis { get; }
    public List<VariableResponse>? Variables { get; }
    public List<LogicFlowResponse>? LogicFlows { get; }

    public DeploymentResponse(
        string id,
        string projectId,
        EnvironmentTypes environment,
        string commitMessage,
        string taskId,
        int version,
        bool canPromote,
        AuditUserDto updatedBy,
        ProjectResponse? project,
        BrandingResponse? branding,
        List<ApiIncludeResponse>? apis,
        List<VariableResponse>? variables,
        List<LogicFlowResponse>? logicFlows)
    {
        Id = id;
        ProjectId = projectId;
        UpdatedBy = updatedBy;
        Environment = environment;
        CommitMessage = commitMessage;
        TaskId = taskId;
        Version = version;
        CanPromote = canPromote;
        Project = project;
        Branding = branding;
        Apis = apis;
        Variables = variables;
        LogicFlows = logicFlows;
    }

    public DeploymentResponse()
    {
    }

    public string TrackingId { get; set; }

    public void SetPages(IEnumerable<DeploymentPage> pages)
    {
        Pages = pages.Select(p => new DeploymentPageResponse(
                p.Id,
                p.ProjectId,
                p.PageId,
                p.DeploymentId,
                p.Environment,
                p.Title,
                p.Slug,
                p.Description,
                p.AuthenticatedOnly,
                p.AuthenticatedUserRole,
                p.PageState,
                p.Actions?.Select(a => new PageActionDto
                {
                    Id = a.Id,
                    Trigger = a.Trigger,
                    Action = Json.Deserialize<object>(a.Action),
                    SequentialTo = a.SequentialTo
                }).ToList(),
                p.Project != null ? ProjectResponse.EntityToResponse(p.Project) : null,
                p.Branding != null ? BrandingResponse.EntityToResponse(p.Branding) : null,
                p.Apis?.Select(ApiIncludeResponse.EntityToResponse()).ToList(),
                p.Variables?.Select(VariableResponse.EntityToResponse()).ToList(),
                p.LogicFlows?.Select(LogicFlowResponse.EntityToResponse()).ToList()
            )
        ).ToList();
    }

    public static Func<Deployment, DeploymentResponse> EntityToResponse(bool canPromote)
    {
        return entity => new DeploymentResponse(
            entity.Id,
            entity.ProjectId,
            entity.Environment,
            entity.CommitMessage,
            entity.TaskId,
            entity.Version,
            canPromote && entity.Environment != EnvironmentTypes.Production,
            new AuditUserDto(entity.AuditInformation),
            entity.Project != null ? ProjectResponse.EntityToResponse(entity.Project) : null,
            entity.Branding != null ? BrandingResponse.EntityToResponse(entity.Branding) : null,
            entity.Apis?.Select(ApiIncludeResponse.EntityToResponse()).ToList(),
            entity.Variables?.Select(VariableResponse.EntityToResponse()).ToList(),
            entity.LogicFlows?.Select(LogicFlowResponse.EntityToResponse()).ToList()
        );
    }

    public static DeploymentResponse EntityToResponse(DeploymentWithPages entity, bool canPromote)
    {
        return new DeploymentResponse(
            entity.Id,
            entity.ProjectId,
            entity.Environment,
            entity.CommitMessage,
            entity.TaskId,
            entity.Version,
            canPromote && entity.Environment != EnvironmentTypes.Production,
            new AuditUserDto(entity.AuditInformation),
            ProjectResponse.EntityToResponse(entity.Project),
            BrandingResponse.EntityToResponse(entity.Branding),
            entity.Apis?.Select(ApiIncludeResponse.EntityToResponse()).ToList(),
            entity.Variables?.Select(VariableResponse.EntityToResponse()).ToList(),
            entity.LogicFlows?.Select(LogicFlowResponse.EntityToResponse()).ToList()
        )
        {
            Pages = entity.Pages.Select(p => new DeploymentPageResponse(
                    p.Id,
                    p.ProjectId,
                    p.PageId,
                    p.DeploymentId,
                    p.Environment,
                    p.Title,
                    p.Slug,
                    p.Description,
                    p.AuthenticatedOnly,
                    p.AuthenticatedUserRole,
                    p.PageState,
                    p.Actions?.Select(a => new PageActionDto
                    {
                        Id = a.Id,
                        Trigger = a.Trigger,
                        Action = Json.Deserialize<object>(a.Action),
                        SequentialTo = a.SequentialTo
                    }).ToList(),
                    ProjectResponse.EntityToResponse(p.Project),
                    BrandingResponse.EntityToResponse(p.Branding),
                    p.Apis?.Select(a => ApiIncludeResponse.EntityToResponse()(a)).ToList(),
                    p.Variables?.Select(v => VariableResponse.EntityToResponse()(v)).ToList(),
                    p.LogicFlows?.Select(l => LogicFlowResponse.EntityToResponse()(l)).ToList()
                )
            ).ToList()
        };
    }

    public static DeploymentResponse EntityToResponse(Deployment model, bool canPromote) =>
        EntityToResponse(canPromote)(model);

    public static Func<DeploymentModel, DeploymentResponse> ModelToResponse(bool canPromote)
    {
        return model => new DeploymentResponse(
            model.Id!,
            model.ProjectId,
            model.Environment,
            model.CommitMessage,
            model.TaskId,
            model.Version,
            canPromote && model.Environment != EnvironmentTypes.Production,
            new AuditUserDto(model.AuditInformation),
            ProjectResponse.ModelToResponse(model.Project),
            BrandingResponse.ModelToResponse(model.Branding),
            model.Apis?.Select(ApiIncludeResponse.ModelToResponse).ToList(),
            model.Variables?.Select(VariableResponse.ModelToResponse).ToList(),
            model.LogicFlows?.Select(LogicFlowResponse.ModelToResponse).ToList()
        );
    }

    public static IResponse ModelToResponse(
        RepositoryActionResultModel<DeploymentModel> actionResult,
        bool canPromote)
    {
        return actionResult.ActionResult(
            actionResult,
            ModelToResponse(canPromote));
    }
}