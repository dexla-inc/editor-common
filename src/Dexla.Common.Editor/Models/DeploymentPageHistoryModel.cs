using Dexla.Common.Editor.Entities;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;

namespace Dexla.Common.Editor.Models;

public class DeploymentPageHistoryModel : IModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }
    public string ProjectId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string DeploymentId { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public string PageId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool AuthenticatedOnly { get; set; }
    public string AuthenticatedUserRole { get; set; } = string.Empty;
    public List<string> PageState { get; set; } = [];
    public long Created { get; set; }
    public ProjectModel Project { get; set; } = new();
    public BrandingModel Branding { get; set; } = new();
    public List<ApiWithApiEndpoints>? Datasources { get; set; }
    public List<VariableModel>? Variables { get; set; }
    public List<LogicFlowModel>? LogicFlows { get; set; }
}