using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;

namespace Dexla.Common.Editor.Entities;

public class DeploymentPage : IEntity
{
    public string Id { get; set; } = string.Empty;
    public EntityStatus EntityStatus { get; set; }
    public string DeploymentId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public virtual bool AuthenticatedOnly { get; set; }
    public virtual string AuthenticatedUserRole { get; set; } = string.Empty;
    public List<string> PageState { get; set; } = [];
}