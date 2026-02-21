using Application.Abstractions.Data;
using Domain.Roles;
using Domain.Users;

namespace Infrastructure.Authorization;

internal sealed class PermissionProvider
    (IApplicationDbContext context)
{
    public async Task<Role> GetForUserIdAsync(Guid userId)
    {
        User user = await context.Users.FindAsync([userId]);

        return user!.Role;
    }
}
