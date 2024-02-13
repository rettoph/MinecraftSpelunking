using Microsoft.AspNetCore.Http;

namespace MinecraftSpelunking.Application.AspNetCore.Common.Services
{
    public interface ICookieApplicationService
    {
        string? Get(string cookie);
        string? Read(string cookie);
        void Set(string cookie, string value, CookieOptions options);
        void Set(string cookie, string value, CookieBuilder builder);
        void Delete(string cookie);
    }
}
