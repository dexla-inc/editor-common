using Dexla.Common.Types.Interfaces;
using Dexla.EditorAPI.Core.Entities;

namespace Dexla.Common.Editor.Responses;

public class TemplateResponse(
        string id,
        string name,
        string state,
        TemplateTypes type,
        TemplateTags[] tags,
        long updatedAt,
        long createdAt)
    : ISuccess
{
    public string Id { get; } = id;
    public string Name { get; } = name;
    public string State { get; } = state;
    public TemplateTypes Type { get; } = type;
    public TemplateTags[] Tags { get; } = tags;
    public long UpdatedAt { get; } = updatedAt;
    public long CreatedAt { get; } = createdAt;

    public string TrackingId { get; set; } = string.Empty;
}