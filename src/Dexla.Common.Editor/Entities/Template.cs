using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.EditorAPI.Core.Entities;

namespace Dexla.Common.Editor.Entities;

public class Template : IEntity
{
    protected Template()
    {
    }

    public string Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    //public string Prompt { get; set; }
    public TemplateTypes Type { get; set; }
    public TemplateTags[] Tags { get; set; }
    public long UpdatedAt { get; set; }
    public long CreatedAt { get; set; }
}