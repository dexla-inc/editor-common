using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Models;

public class TemplateModel : IModel
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string Name { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string[] Tags { get; set; } = Array.Empty<string>();
    public long UpdatedAt { get; set; }
    public long CreatedAt { get; set; }

    public void ReformatOnCreate(string? tags)
    {
        CsvToArray(tags);
        UpdateNameToCamelCase();
        SetCreatedAt();
        SetUpdatedAt();
    }

    public void ReformatOnUpdate(string? tags)
    {
        CsvToArray(tags);
        UpdateNameToCamelCase();
        SetUpdatedAt();
    }

    private void CsvToArray(string? value)
    {
        Tags = value?.Split(',') ?? Array.Empty<string>();
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