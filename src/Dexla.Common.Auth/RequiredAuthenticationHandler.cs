using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace Dexla.Common.Auth;

public class RequiredAuthenticationHandler : AuthorizationHandler<RequiredAuthentication>
{
    private readonly IUserTokenService _userTokenServiceService;

    public RequiredAuthenticationHandler(IUserTokenService userTokenServiceService)
    {
        _userTokenServiceService = userTokenServiceService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        RequiredAuthentication requirement)
    {
        Claim? userId = context.User.FindFirst("user_id");

        if (userId != null)
        {
            context.Succeed(requirement);
            _userTokenServiceService.SetValues(context.User);
        }

        return Task.CompletedTask;
    }
}
