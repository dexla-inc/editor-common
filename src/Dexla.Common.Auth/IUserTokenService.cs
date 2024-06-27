using System.Security.Claims;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Auth;

public interface IUserTokenService
{
    string UserId { get; }
    List<AuthCompany> Companies { get; }
    UserRoles UserRole { get; }
    void SetValues(ClaimsPrincipal userClaims);
    string GetUserId();
    string GetName();
    UserRoles GetUserRole(string companyId);
    IEnumerable<AuthCompany> GetCompanies();
    bool TryParseCompany(string companyId, out AuthCompany? authCompany);
    bool IsDexlaAdmin(string companyId);
}