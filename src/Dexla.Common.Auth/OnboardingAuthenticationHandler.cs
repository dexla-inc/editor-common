using Dexla.Common.Auth;
using Microsoft.AspNetCore.Authorization;

public class OnboardingAuthenticationHandler(IUserTokenService userTokenServiceService)
    : AuthorizationHandler<OnboardingAuthentication>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OnboardingAuthentication requirement)
    {
        bool? isAuthenticated = context.User.Identity?.IsAuthenticated;
        
        if (isAuthenticated == true)
        {
            context.Succeed(requirement);
            userTokenServiceService.SetValues(context.User);
        }

        return Task.CompletedTask;
    }
}