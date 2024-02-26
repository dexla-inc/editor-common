using System.Collections.Generic;

namespace Dexla.Common.Editor.Entities;

public class DeploymentPage
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public virtual bool AuthenticatedOnly { get; set; }
    public virtual string AuthenticatedUserRole { get; set; } = string.Empty;
    public List<string> PageState { get; set; } = [];
}