using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Entities;

public class ProjectCollaborator
{
    public virtual string Email { get; set; } = string.Empty;
    public virtual string? UserId { get; set; }
    public virtual UserRoles AccessLevel { get; set; }
    public virtual TeamStatus Status { get; set; }
}