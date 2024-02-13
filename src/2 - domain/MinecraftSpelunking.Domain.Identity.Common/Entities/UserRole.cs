using Microsoft.AspNetCore.Identity;
using MinecraftSpelunking.Domain.Identity.Common.Enums;

namespace MinecraftSpelunking.Domain.Identity.Common.Entities
{
    public class UserRole : IdentityRole<int>
    {
        public UserRoleTypeEnum Type { get; set; }

        public static implicit operator UserRoleTypeEnum(UserRole role)
        {
            return role.Type;
        }

        public static string GetName(UserRoleTypeEnum role)
        {
            return role.ToString();
        }
    }
}
