using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Entities;

public class Component : IEntity
{
    protected Component()
    {
        
    }

    public string Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public BasicAuditInformation? AuditInformation { get; set; }
    public string UserId { get; set; }
    public string ProjectId { get; set; }
    public string CompanyId { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public UITypes Type { get; set; }
    public string? ImagePreviewUrl { get; set; }
    public ComponentScopes Scope { get; set; }
    
    public void SetType(UITypes type)
    {
        Type = type;
    }
}