﻿using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class DeploymentPageHistoryResponse : ISuccess
{
    public string Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public bool AuthenticatedOnly { get; }
    public string AuthenticatedUserRole { get; }
    public string? PageState { get; set; }
    public string PageId { get; }
    public long Created { get; }

    public DeploymentPageHistoryResponse(
        string id,
        string pageId,
        string title,
        string slug,
        bool authenticatedOnly,
        string authenticatedUserRole,
        IEnumerable<string> pageState,
        long created)
    {
        Id = id;
        PageId = pageId;
        Title = title;
        Slug = slug;
        AuthenticatedOnly = authenticatedOnly;
        AuthenticatedUserRole = authenticatedUserRole;
        PageState = string.Join("", pageState);
        Created = created;
    }


    public string TrackingId { get; set; }
}