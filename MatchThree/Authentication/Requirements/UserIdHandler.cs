using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace MatchThree.API.Authentication.Requirements;

public class UserIdHandler : AuthorizationHandler<UserIdRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIdRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext) 
            return Task.CompletedTask;
        
        var userIdClaim = context.User.FindFirst(JwtRegisteredClaimNames.Sub);
        var userIdFromRoute = httpContext.Request.RouteValues["userId"]?.ToString(); //TODO get rid of the magic string

        if (userIdClaim != null && userIdClaim.Value == userIdFromRoute)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}