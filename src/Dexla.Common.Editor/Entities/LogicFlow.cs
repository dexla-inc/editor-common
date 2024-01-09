using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;

namespace Dexla.Common.Editor.Entities;

public class LogicFlow : IEntity
{
    protected LogicFlow()
    {
    }

    public string Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; }
    public string ProjectId { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
    public long CreatedAt { get; set; }
    public long UpdatedAt { get; set; }
}
