using Microsoft.AspNetCore.Authorization;

namespace Dexla.Common.Auth;

public class RequiredAuthentication : IAuthorizationRequirement
{
}

public class TestAuthentication : IAuthorizationRequirement
{
}