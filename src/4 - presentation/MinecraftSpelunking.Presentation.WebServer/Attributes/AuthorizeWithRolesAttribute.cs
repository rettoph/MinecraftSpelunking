using Microsoft.AspNetCore.Authorization;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Identity.Common.Enums;

namespace MinecraftSpelunking.Presentation.WebServer.Attributes
{
    public sealed class AuthorizeWithRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeWithRolesAttribute(params UserRoleTypeEnum[] roles) : base()
        {
            this.Roles = string.Join(",", roles.Select(x => UserRole.GetName(x)));
        }
    }
}
