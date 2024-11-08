﻿using System.Security.Claims;
using Dexla.Common.Types;
using Dexla.Common.Types.Enums;

namespace Dexla.Common.Auth;

public class UserTokenService : IUserTokenService
{
    public string UserId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public List<AuthCompany> Companies { get; private set; } = [];
    public UserRoles UserRole { get; private set; }
    public string GetUserId() => UserId;
    public string GetName() => Name;
    public bool Onboarding { get; private set; }
    
    public UserRoles GetUserRole(string companyId)
    {
        return Companies.FirstOrDefault(c => c.Id == companyId)?.UserRole ?? UserRoles.MEMBER;
    }
    
    public bool IsDexlaAdmin(string companyId) => GetUserRole(companyId) == UserRoles.DEXLA_ADMIN;

    public IEnumerable<AuthCompany> GetCompanies() => Companies;
    public bool TryParseCompany(string companyId, out AuthCompany? authCompany)
    {
        authCompany = Companies.FirstOrDefault(c => c.Id == companyId);
        return authCompany != null;
    }

    public void SetValues(ClaimsPrincipal userClaims, bool onboarding = false)
    {
        List<Claim> claims = userClaims.Claims.ToList();

        string? orgInfoString = claims.FirstOrDefault(c => c.Type == "org_id_to_org_member_info")?.Value;
        if (orgInfoString != null)
        {
            Dictionary<string, PropelAuthOrgInfo> orgDictionary = 
                Json.Deserialize<Dictionary<string, PropelAuthOrgInfo>>(orgInfoString, Casing.SNAKE_CASE);
        
            List<PropelAuthOrgInfo> organisation = orgDictionary.Values.ToList();
        
            Companies = organisation.Select(orgInfo => new AuthCompany(
                orgInfo.OrgId,
                orgInfo.UrlSafeOrgName,
                orgInfo.OrgName,
                Enum.Parse<UserRoles>(orgInfo.UserRole),
                orgInfo.GetProjectIds(),
                orgInfo.AdditionalRoles,
                orgInfo.InheritedUserRolesPlusCurrentRole)
            ).ToList();
        }
        
        UserId = claims.FirstOrDefault(c => c.Type == "user_id")?.Value ?? string.Empty;
        string? firstName = claims.FirstOrDefault(c => c.Type == "first_name")?.Value;
        string? lastName = claims.FirstOrDefault(c => c.Type == "last_name")?.Value;
        Name = $"{firstName} {lastName}";
        
        Onboarding = onboarding;

        if (onboarding)
        {
            Companies =
            [
                new AuthCompany(
                    UserId,
                    "onboarding-" + UserId,
                    "Onboarding" + UserId,
                    UserRoles.GUEST,
                    [],
                    [],
                    []
                )
            ];
        }
    }
}