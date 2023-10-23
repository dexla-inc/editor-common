using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Responses;

public class ProjectCollaboratorDto
{
    public string? Email { get; set; }
    public string? UserId { get; set; }
    public UserRoles AccessLevel { get; set; }
    public TeamStatus Status { get; set; }

    public ProjectCollaboratorDto(string? email, string? userId, UserRoles accessLevel, TeamStatus status)
    {
        Email = email;
        UserId = userId;
        AccessLevel = accessLevel;
        Status = status;
    }

    public ProjectCollaboratorDto()
    {
        
    }
}