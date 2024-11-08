﻿using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class DeploymentPageResponse : ISuccess
{
    public string Id { get; }
    public string ProjectId { get; }
    public string DeploymentId { get; }
    public string PageId { get; }
    public EnvironmentTypes Environment { get; }
    public string Title { get; }
    public string Slug { get; }
    public string? Description { get; }
    public bool AuthenticatedOnly { get; }
    public string AuthenticatedUserRole { get; }
    public string? PageState { get; set; }
    public List<PageActionDto>? Actions { get; }
    public ProjectResponse? Project { get; }
    public BrandingResponse? Branding { get; }
    public List<ApiIncludeResponse>? Datasources { get; }
    public List<VariableResponse>? Variables { get; }
    public List<LogicFlowResponse>? LogicFlows { get; }


    public DeploymentPageResponse(
        string id,
        string projectId,
        string deploymentId,
        string pageId,
        EnvironmentTypes environment,
        string title,
        string slug,
        string? description,
        bool authenticatedOnly,
        string authenticatedUserRole,
        IEnumerable<string> pageState,
        List<PageActionDto>? actions,
        ProjectResponse? project,
        BrandingResponse? branding,
        List<ApiIncludeResponse>? datasources,
        List<VariableResponse>? variables,
        List<LogicFlowResponse>? logicFlows)
    {
        Id = id;
        ProjectId = projectId;
        DeploymentId = deploymentId;
        PageId = pageId;
        Environment = environment;
        Title = title;
        Slug = slug;
        Description = description;
        AuthenticatedOnly = authenticatedOnly;
        AuthenticatedUserRole = authenticatedUserRole;
        PageState = string.Join("", pageState);
        Actions = actions;
        Project = project;
        Branding = branding;
        Datasources = datasources;
        Variables = variables;
        LogicFlows = logicFlows;
    }


    public string TrackingId { get; set; }

    public static Func<DeploymentPage, DeploymentPageResponse> EntityToResponse(bool? exclude)
    {
        return entity => new DeploymentPageResponse(
            entity.Id,
            entity.ProjectId,
            entity.PageId,
            entity.DeploymentId,
            entity.Environment,
            entity.Title,
            entity.Slug,
            entity.Description,
            entity.AuthenticatedOnly,
            entity.AuthenticatedUserRole,
            entity.PageState,
            entity.Actions?.Select(a => new PageActionDto
            {
                Id = a.Id,
                Trigger = a.Trigger,
                Action = Json.Deserialize<object>(a.Action),
                SequentialTo = a.SequentialTo
            }).ToList(),
            entity.Project != null && exclude != true ? ProjectResponse.EntityToResponse(entity.Project) : null,
            entity.Branding != null && exclude != true ? BrandingResponse.EntityToResponse(entity.Branding) : null,
            entity.Datasources?.Select(api => ApiIncludeResponse.EntityToResponse()(api)).ToList(),
            entity.Variables?.Select(v => VariableResponse.EntityToResponse()(v)).ToList(),
            entity.LogicFlows?.Select(l => LogicFlowResponse.EntityToResponse()(l)).ToList()
        );
    }

    public static DeploymentPageResponse EntityToResponse(DeploymentPage entity, bool exclude = false)
    {
        return EntityToResponse(exclude)(entity);
    }

    public static DeploymentPageResponse Empty(ProjectResponse? project = null, BrandingResponse? branding = null) =>
        new(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            EnvironmentTypes.Editor,
            string.Empty,
            string.Empty,
            string.Empty,
            false,
            string.Empty,
            new List<string>(),
            [],
            project,
            branding,
            null,
            null,
            null);

    public static DeploymentPageResponse InvalidResponse(ProjectWithBrandingResponse project) => new(
        string.Empty,
        project.Id,
        string.Empty,
        string.Empty,
        EnvironmentTypes.Editor,
        string.Empty,
        string.Empty,
        string.Empty,
        false,
        string.Empty,
        new List<string>(),
        null,
        project,
        project.Branding,
        null,
        null,
        null);
}