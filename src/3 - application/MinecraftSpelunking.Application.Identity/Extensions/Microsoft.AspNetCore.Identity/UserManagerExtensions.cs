using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Identity.Common.Enums;

namespace Microsoft.AspNetCore.Identity
{
    public static class UserManagerExtensions
    {
        public static async Task<IdentityResult> AddToRolesAsync(this UserManager<User> userManager, User user, params UserRoleTypeEnum[] roles)
        {
            return await userManager.AddToRolesAsync(user, roles.Select(UserRole.GetName));
        }
    }
}
