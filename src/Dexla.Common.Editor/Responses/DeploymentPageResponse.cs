﻿using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class DeploymentPageResponse : ISuccess
{
    public string Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public bool AuthenticatedOnly { get; }
    public string AuthenticatedUserRole { get; }
    public string? PageState { get; set; }
    public List<PageActionDto>? Actions { get; set; }
    public ProjectResponse? Project { get; set; }
    public BrandingResponse? Branding { get; set; }

    public DeploymentPageResponse(
        string id,
        string title,
        string slug,
        bool authenticatedOnly,
        string authenticatedUserRole,
        IEnumerable<string> pageState,
        List<PageActionDto>? actions,
        ProjectResponse? project,
        BrandingResponse? branding)
    {
        Id = id;
        Title = title;
        Slug = slug;
        AuthenticatedOnly = authenticatedOnly;
        AuthenticatedUserRole = authenticatedUserRole;
        PageState = string.Join("", pageState);
        Actions = actions;
        Project = project;
        Branding = branding;
    }


    public string TrackingId { get; set; }

    public static Func<DeploymentPage, DeploymentPageResponse> EntityToResponse(bool? exclude)
    {
        return entity => new DeploymentPageResponse(
            entity.Id,
            entity.Title,
            entity.Slug,
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
            entity.Branding != null && exclude != true ? BrandingResponse.EntityToResponse(entity.Branding) : null);
    }

    public static DeploymentPageResponse EntityToResponse(DeploymentPage entity, bool exclude = false)
    {
        return EntityToResponse(exclude)(entity);
    }

    public static DeploymentPageResponse Empty => new(
        string.Empty,
        string.Empty,
        string.Empty,
        false,
        string.Empty,
        new List<string>(),
        new List<PageActionDto>(),
        null,
        null);
}