using Microsoft.AspNetCore.Identity;

namespace MinecraftSpelunking.Common.Account.Entities
{
    public class User : IdentityUser<int>
    {
        public string? ApiAccessToken { get; set; }
    }
}
