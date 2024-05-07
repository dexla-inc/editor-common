using Dexla.Common.Editor.Models;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;
using Dexla.Common.Utilities;

namespace Dexla.Common.Editor.Entities;

public class Deployment : IEntity, IEntityWithTimeStamp
{
    protected Deployment()
    {
    }

    public virtual string Id { get; set; } = UtilityExtensions.GetId();

    public virtual EntityStatus EntityStatus { get; set; }
    public virtual long Timestamp { get; set; } = DateTimeExtensions.GetTimestamp();
    public virtual string UserId { get; set; }
    public virtual string ProjectId { get; set; }
    public virtual string CommitMessage { get; set; }
    public virtual string TaskId { get; set; }
    public virtual EnvironmentTypes Environment { get; set; }
    public virtual int Version { get; set; }
    public virtual ProjectModel? Project { get; set; }
    public virtual BrandingModel? Branding { get; set; }
}