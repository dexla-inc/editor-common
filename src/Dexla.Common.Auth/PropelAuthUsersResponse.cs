using Dexla.Common.Types.Interfaces;

namespace Dexla.Common.Auth;

public class PropelAuthUsersResponse : IResponse
{
    public List<PropelAuthUserResponse> Users { get; set; } = new();
}

public class PropelAuthUserResponse
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public bool HasPassword { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public Dictionary<string, object> Properties { get; set; } = new();
    public object Metadata { get; set; } = new();
    public bool Locked { get; set; }
    public bool Enabled { get; set; }
    public bool MfaEnabled { get; set; }
    public bool CanCreateOrgs { get; set; }
    public long CreatedAt { get; set; }
    public long LastActiveAt { get; set; }
    public bool UpdatePasswordRequired { get; set; }
    public Dictionary<string, PropelAuthOrgInfo> OrgIdToOrgInfo { get; set; } = new();
}