using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class DeploymentPageResponse : ISuccess
{
    public string Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public bool AuthenticatedOnly { get; }
    public string AuthenticatedUserRole { get; }

    public DeploymentPageResponse(
        string id,
        string title,
        string slug,
        bool authenticatedOnly,
        string authenticatedUserRole)
    {
        Id = id;
        Title = title;
        Slug = slug;
        AuthenticatedOnly = authenticatedOnly;
        AuthenticatedUserRole = authenticatedUserRole;
    }

    public string? PageState { get; set; }

    public string TrackingId { get; set; }

    public static DeploymentPageResponse Empty => new(
        string.Empty, 
        string.Empty, 
        string.Empty, 
        false, 
        string.Empty);
}