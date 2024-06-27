using Dexla.Common.Types.Enums;

namespace Dexla.Common.Auth;

public record AuthCompany(
    string Id, 
    string Name, 
    string FriendlyName, 
    UserRoles UserRole, 
    List<string> ProjectIds,
    List<string> AdditionalRoles,
    List<string> InheritedUserRolesPlusCurrentRole);