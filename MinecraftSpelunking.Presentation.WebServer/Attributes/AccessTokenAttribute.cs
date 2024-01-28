using MinecraftSpelunking.Common.Account;

namespace MinecraftSpelunking.Presentation.WebServer.Attributes
{
    public class AccessTokenAttribute : Attribute
    {
        public readonly UserRoleTypeEnum[] RequiredRoles;

        public AccessTokenAttribute(params UserRoleTypeEnum[] requiredRoles)
        {
            RequiredRoles = requiredRoles;
        }
    }
}
