using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Entities;

public class Variable : IEntity
{
    protected Variable()
    {
    }

    public string Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; }
    public string ProjectId { get; set; }
    public string Name { get; set; }
    public FrontEndTypes Type { get; set; }
    public string DefaultValue { get; set; }
}