using System.Security.Claims;
using Dexla.Common.Auth;
using Microsoft.AspNetCore.Authorization;

public class OnboardingAuthenticationHandler(IUserTokenService userTokenServiceService)
    : AuthorizationHandler<OnboardingAuthentication>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OnboardingAuthentication requirement)
    {
        Claim? iss = context.User.FindFirst("iss");
        
        if(iss?.Value == "dexla.ai")
        {
            context.Succeed(requirement);
            userTokenServiceService.SetValues(context.User, true);
        }

        return Task.CompletedTask;
    }
}