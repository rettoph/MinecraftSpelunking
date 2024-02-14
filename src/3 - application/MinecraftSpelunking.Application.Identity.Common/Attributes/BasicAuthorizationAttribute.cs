using Microsoft.AspNetCore.Authorization;

namespace MinecraftSpelunking.Application.Identity.Common.Attributes
{
    public sealed class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            this.AuthenticationSchemes = Constants.AuthenticationSchemas.Basic;
        }
    }
}
