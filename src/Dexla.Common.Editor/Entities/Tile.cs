using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;

namespace Dexla.Common.Editor.Entities;

public class Tile : IEntity
{
    protected Tile()
    {
    }

    public string Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public string Prompt { get; set; }
    public string TemplateId { get; set; }
    public long UpdatedAt { get; set; }
    public long CreatedAt { get; set; }
}