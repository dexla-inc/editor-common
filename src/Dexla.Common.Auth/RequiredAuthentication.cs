using Microsoft.AspNetCore.Authorization;

namespace Dexla.Common.Auth;

public class RequiredAuthentication : IAuthorizationRequirement;

public class OnboardingAuthentication : IAuthorizationRequirement;

public class RequiredAuthenticationOrOnboardingAuthentication : IAuthorizationRequirement;