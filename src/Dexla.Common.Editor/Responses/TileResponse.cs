using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Editor.Responses;

public class TileResponse(
        string id,
        string name,
        string state,
        string prompt,
        string templateId,
        long updatedAt,
        long createdAt)
    : ISuccess
{
    public string Id { get; } = id;
    public string Name { get; } = name;
    public string State { get; } = state;
    public string Prompt { get; } = prompt;
    public string TemplateId { get; } = templateId;
    public long UpdatedAt { get; } = updatedAt;
    public long CreatedAt { get; } = createdAt;

    public string TrackingId { get; set; } = string.Empty;
}