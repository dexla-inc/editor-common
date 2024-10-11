using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Entities;

public class DeploymentPageHistory : IEntity
{
    public string Id { get; set; } = UtilityExtensions.GetId();
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string DeploymentId { get; set; } = string.Empty;
    public EnvironmentTypes Environment { get; set; } = EnvironmentTypes.Editor;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool AuthenticatedOnly { get; set; }
    public string AuthenticatedUserRole { get; set; } = string.Empty;
    public List<string> PageState { get; set; } = [];
    public virtual List<PageAction>? Actions { get; set; }
    public virtual string PageId { get; set; } = string.Empty;
    public virtual long Created { get; set; }
    public Project? Project { get; set; }
    public Branding? Branding { get; set; }
    public virtual List<ApiWithApiEndpoints>? Datasources { get; set; }
    public virtual List<Variable>? Variables { get; set; }
    public virtual List<LogicFlow>? LogicFlows { get; set; }
}