using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Entities;

public class Deployment : IEntity, IEntityWithTimeStamp
{
    public Deployment()
    {
        
    }
    public string Id { get; set; } = UtilityExtensions.GetId();
    
    public EntityStatus EntityStatus { get; set; }
    public long Timestamp { get; set; } = DateTimeExtensions.GetTimestamp();
    public virtual string UserId { get; set; }
    public virtual string ProjectId { get; set; }
    public virtual string CommitMessage { get; set; }
    public virtual string TaskId { get; set; }
    public virtual EnvironmentTypes Environment { get; set; }
    public virtual int Version { get; set; }
    public List<DeploymentPage> Pages { get; set; } = [];
}