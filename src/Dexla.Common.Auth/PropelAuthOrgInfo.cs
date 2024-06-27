namespace Dexla.Common.Auth;

public class PropelAuthOrgInfo
{
    public string OrgId { get; set; } = string.Empty;
    public string OrgName { get; set; } = string.Empty;
    public string OrgRoleStructure { get; set; } = string.Empty;
    public string UrlSafeOrgName { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;
    public List<string> InheritedUserRolesPlusCurrentRole { get; set; } = [];
    public List<string> UserPermissions { get; set; } = [];
    public List<string> AdditionalRoles { get; set; } = [];
    public Dictionary<string, string> OrgMetadata { get; set; } = new();

    public List<string> GetProjectIds()
    {
        return OrgMetadata.ContainsKey("projectIds") == true
            ? OrgMetadata["projectIds"].Split(',').ToList()
            : [];

    }
}