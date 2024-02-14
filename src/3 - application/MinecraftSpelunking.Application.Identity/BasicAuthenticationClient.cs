using System.Security.Principal;

namespace MinecraftSpelunking.Application.Identity
{
    internal sealed class BasicAuthenticationClient : IIdentity
    {
        public string? AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string? Name { get; set; }
    }
}
