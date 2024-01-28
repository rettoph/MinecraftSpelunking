using Microsoft.AspNetCore.Identity;

namespace MinecraftSpelunking.Common.Account.Entities
{
    public class UserRole : IdentityRole<int>
    {
        public UserRoleTypeEnum Type { get; set; }

        public static implicit operator UserRoleTypeEnum(UserRole role)
        {
            return role.Type;
        }
    }
}
