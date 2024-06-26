using System.Text.Json.Serialization;
using Dexla.Common.Editor.Entities;
using Dexla.Common.Types.Enums;
using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class DeploymentPageHistoryResponse : ISuccess
{
    public string Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public bool AuthenticatedOnly { get; }
    public string AuthenticatedUserRole { get; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PageState { get; set; }
    public string PageId { get; }
    public EnvironmentTypes Environment { get; }
    public long Created { get; }

    public DeploymentPageHistoryResponse(
        string id,
        string pageId,
        string title,
        string slug,
        bool authenticatedOnly,
        string authenticatedUserRole,
        IEnumerable<string> pageState,
        EnvironmentTypes environment,
        long created)
    {
        Id = id;
        PageId = pageId;
        Title = title;
        Slug = slug;
        AuthenticatedOnly = authenticatedOnly;
        AuthenticatedUserRole = authenticatedUserRole;
        PageState = string.Join("", pageState);
        Environment = environment;
        Created = created;
    }

    protected DeploymentPageHistoryResponse(
        string id,
        string pageId,
        string title,
        string slug,
        bool authenticatedOnly,
        string authenticatedUserRole,
        EnvironmentTypes environment,
        long created)
    {
        Id = id;
        PageId = pageId;
        Title = title;
        Slug = slug;
        AuthenticatedOnly = authenticatedOnly;
        AuthenticatedUserRole = authenticatedUserRole;
        Environment = environment;
        Created = created;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string TrackingId { get; set; }

    public static Func<DeploymentPageHistory, DeploymentPageHistoryResponse> EntityToResponse(string? include)
    {
        if (include != null && include.Contains(nameof(PageState), StringComparison.InvariantCultureIgnoreCase))
            return entity => new DeploymentPageHistoryResponse(
                entity.Id,
                entity.PageId,
                entity.Title,
                entity.Slug,
                entity.AuthenticatedOnly,
                entity.AuthenticatedUserRole,
                entity.PageState,
                entity.Environment,
                entity.Created);
        
        return entity => new DeploymentPageHistoryResponse(
            entity.Id,
            entity.PageId,
            entity.Title,
            entity.Slug,
            entity.AuthenticatedOnly,
            entity.AuthenticatedUserRole,
            entity.Environment,
            entity.Created);
    }
}