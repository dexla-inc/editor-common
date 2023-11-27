using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class TileModel : IModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string Name { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Prompt { get; set; } = string.Empty;
    public string TemplateId { get; set; } = string.Empty;
    public long UpdatedAt { get; set; }
    public long CreatedAt { get; set; }

    public void SetValues(string templateId)
    {
        TemplateId = templateId;
        UpdateNameToCamelCase();
        SetCreatedAt();
        SetUpdatedAt();
    }
    
    private void UpdateNameToCamelCase()
    {
        Name = Name.ToCamelCase();
    }
    private void SetCreatedAt()
    {
        CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    private void SetUpdatedAt()
    {
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}