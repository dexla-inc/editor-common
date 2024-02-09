using Dexla.Common.Editor.Models;
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
    public string PageState { get; }
    public bool IsHome { get; }
    public bool AuthenticatedOnly { get; }
    public string AuthenticatedUserRole { get; }
    public string? ParentPageId { get; }
    public bool HasNavigation { get; }
    public Dictionary<string,string>? QueryStrings { get; }
    public List<ActionDto>? Actions { get; set; }

    public PageResponse(
        string? id,
        string projectId,
        string title,
        string slug,
        string description,
        string pageState,
        bool isHome,
        bool authenticatedOnly,
        string authenticatedUserRole,
        string? parentPageId,
        bool hasNavigation,
        Dictionary<string, string>? queryStrings,
        List<ActionDto>? actions)
    {
        Id = id;
        ProjectId = projectId;
        Title = title;
        Description = description;
        Slug = slug;
        PageState = pageState;
        IsHome = isHome;
        AuthenticatedOnly = authenticatedOnly;
        AuthenticatedUserRole = authenticatedUserRole;
        ParentPageId = parentPageId;
        HasNavigation = hasNavigation;
        QueryStrings = queryStrings;
        Actions = actions;
    }

    public string TrackingId { get; set; }
}