using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Responses;

public class ComponentResponse
{
    public string Id { get; }
    public string Type { get; }
    public string Description { get; }
    public string Content { get; }
    public string? ImagePreviewUrl { get; }
    public ComponentScopes Scope { get; }

    public ComponentResponse(
        string id,
        string type,
        string description,
        string content,
        string? imagePreviewUrl,
        ComponentScopes scope)
    {
        Id = id;
        Type = type;
        Description = description;
        Content = content;
        ImagePreviewUrl = imagePreviewUrl;
        Scope = scope;
    }
}