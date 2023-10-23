using Dexla.Common.Editor.Responses;
using Dexla.Common.Repository.Types.Enums;
using Dexla.Common.Repository.Types.Interfaces;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Editor.Models;

public class ProjectModel : IModelWithUserId
{
    public string? Id { get; set; }
    public EntityStatus EntityStatus { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FriendlyName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? SimilarCompany { get; set; }
    public List<ProjectCollaboratorDto> Collaborators { get; set; } = new();
    public string Domain { get; set; } = string.Empty;
    public string SubDomain { get; set; } = string.Empty;
    public long Created { get; set; }

    public void SetUserId(string value)
    {
        UserId = value;
    }

    public void SetName(string value)
    {
        Name = value;
    }

    public void SetFriendlyName(string value)
    {
        FriendlyName = value;
    }
    
    public void AddOwnerAsCollaborator(string userId)
    {
        Collaborators.Add(new ProjectCollaboratorDto
        {
            UserId = userId,
            AccessLevel = UserRoles.OWNER,
            Status = TeamStatus.ACCEPTED
        });
    }

    public void InviteCollaborator(string email, UserRoles accessLevel)
    {
        Collaborators.Add(new ProjectCollaboratorDto
        {
            Email = email,
            AccessLevel = accessLevel
        });
    }

    public void SetCollaboratorUserId(string email, string userId)
    {
        ProjectCollaboratorDto? collaborator =
            Collaborators.FirstOrDefault(collaboratorDto => collaboratorDto.Email == email);
        
        if (collaborator != null)
        {
            collaborator.UserId = userId;
            collaborator.Status = TeamStatus.ACCEPTED;
        }
    }
    
    public void SetCollaborators(List<ProjectCollaboratorDto> value)
    {
        Collaborators = value;
    }
}