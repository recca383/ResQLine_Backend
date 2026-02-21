using Domain.Roles;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User is { Identity.IsAuthenticated: false })
        {
            context.Fail();

            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();

        PermissionProvider permissionProvider = scope.ServiceProvider.GetRequiredService<PermissionProvider>();

        Guid userId = context.User.GetUserId();

        Role role = await permissionProvider.GetForUserIdAsync(userId);

        if (role.Name.Equals(requirement.Permission, StringComparison.OrdinalIgnoreCase))
        {
            context.Succeed(requirement);
        }
    }
}
