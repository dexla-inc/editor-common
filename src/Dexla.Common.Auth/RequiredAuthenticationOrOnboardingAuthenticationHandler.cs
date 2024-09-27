using System.Security.Claims;
using Dexla.Common.Auth;
using Microsoft.AspNetCore.Authorization;

public class RequiredAuthenticationOrOnboardingAuthenticationHandler(IUserTokenService userTokenServiceService)
    : AuthorizationHandler<RequiredAuthenticationOrOnboardingAuthentication>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RequiredAuthenticationOrOnboardingAuthentication requirement)
    {
        Claim? userId = context.User.FindFirst("user_id");
        Claim? iss = context.User.FindFirst("iss");
        if (userId != null && iss?.Value != "dexla.ai")
        {
            context.Succeed(requirement);
            userTokenServiceService.SetValues(context.User);
        }
        else if(iss?.Value == "dexla.ai")
        {
            context.Succeed(requirement);
            userTokenServiceService.SetValues(context.User, true);
        }

        return Task.CompletedTask;
    }
}