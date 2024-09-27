﻿using Dexla.Common.Editor.Entities;
using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Models;
using Dexla.Common.Types;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class PagesResponse : ISuccess
{
    public string HomePageId { get; set; }

    public PagesResponse(string homePageId)
    {
        HomePageId = homePageId;
    }

    public string TrackingId { get; set; }
}

public class PageResponse : ISuccess
{
    public string? Id { get; }
    public string ProjectId { get; }
    public string Title { get; }
    public string Slug { get; }
    public string Description { get; }
    public bool IsHome { get; }
    public bool AuthenticatedOnly { get; }
    public string AuthenticatedUserRole { get; }
    public string? ParentPageId { get; }
    public bool HasNavigation { get; }
    public Dictionary<string,string>? QueryStrings { get; }
    public List<PageActionDto>? Actions { get; set; }
    public List<string>? Features { get; set; }

    public PageResponse(
        string? id,
        string projectId,
        string title,
        string slug,
        string description,
        bool isHome,
        bool authenticatedOnly,
        string authenticatedUserRole,
        string? parentPageId,
        bool hasNavigation,
        Dictionary<string, string>? queryStrings,
        List<PageActionDto>? actions,
        List<string>? features)
    {
        Id = id;
        ProjectId = projectId;
        Title = title;
        Description = description;
        Slug = slug;
        IsHome = isHome;
        AuthenticatedOnly = authenticatedOnly;
        AuthenticatedUserRole = authenticatedUserRole;
        ParentPageId = parentPageId;
        HasNavigation = hasNavigation;
        QueryStrings = queryStrings;
        Actions = actions;
        Features = features;
    }

    public string TrackingId { get; set; }
    
    public static Func<Page, PageResponse> EntityToResponse()
    {
        return page => new PageResponse(
            page.Id,
            page.ProjectId,
            page.Title,
            page.Slug,
            page.Description,
            page.IsHome,
            page.AuthenticatedOnly,
            page.AuthenticatedUserRole,
            page.ParentPageId,  
            page.HasNavigation,
            page.QueryStrings,
            page.Actions?.Select(a => new PageActionDto
            {
                Id = a.Id,
                Trigger = a.Trigger,
                Action = Json.Deserialize<object>(a.Action),
                SequentialTo = a.SequentialTo
            }).ToList(),
            page.Features);
    }

    public static PageResponse ModelToResponse(PageModel model)
    {
        return new PageResponse(
            model.Id,
            model.ProjectId,
            model.Title,
            model.Slug,
            model.Description,
            model.IsHome,
            model.AuthenticatedOnly,
            model.AuthenticatedUserRole,
            model.ParentPageId,
            model.HasNavigation,
            model.QueryStrings,
            model.Actions,
            model.Features);
    }
    
    public static IResponse ModelToResponse(RepositoryActionResultModel<PageModel>  actionResult)
    {
        return actionResult.ActionResult(
            actionResult,
            ModelToResponse);
    }
}