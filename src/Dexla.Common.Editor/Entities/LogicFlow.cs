using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Entities;

public class LogicFlow : IProjectEntity
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
    public IProjectEntity SetNewValues(string userId, string projectId)
    {
        UserId = userId;
        ProjectId = projectId;
        Id = UtilityExtensions.GetId();
        return this;
    }
}
