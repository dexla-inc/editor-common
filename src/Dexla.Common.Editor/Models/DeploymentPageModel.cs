using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;

namespace Dexla.Common.Editor.Models;

public class DeploymentPageModel : IModel
{
    public string Id { get; set; } = string.Empty;
    public string DeploymentId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public virtual bool AuthenticatedOnly { get; set; }
    public virtual string AuthenticatedUserRole { get; set; } = string.Empty;
    public List<string> PageState { get; set; } = [];
    public EntityStatus EntityStatus { get; set; }
    public long Timestamp { get; set; }
}