using Dexla.Common.Editor.Entities;
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

    public DeploymentPageResponse(
        string id,
        string title,
        string slug,
        bool authenticatedOnly,
        string authenticatedUserRole,
        IEnumerable<string> pageState)
    {
        Id = id;
        Title = title;
        Slug = slug;
        AuthenticatedOnly = authenticatedOnly;
        AuthenticatedUserRole = authenticatedUserRole;
        PageState = string.Join("", pageState);
    }


    public string TrackingId { get; set; }
    
    public static Func<DeploymentPage, DeploymentPageResponse> EntityToResponse()
    {
        return entity => new DeploymentPageResponse(
            entity.Id,
            entity.Title,
            entity.Slug,
            entity.AuthenticatedOnly,
            entity.AuthenticatedUserRole,
            entity.PageState);
    }
    
    public static DeploymentPageResponse EntityToResponse(DeploymentPage entity)
    {
        return EntityToResponse()(entity);
    }

    public static DeploymentPageResponse Empty => new(
        string.Empty,
        string.Empty,
        string.Empty,
        false,
        string.Empty,
        new List<string>());
}