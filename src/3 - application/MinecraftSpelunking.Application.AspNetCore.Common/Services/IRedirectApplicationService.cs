using MinecraftSpelunking.Application.AspNetCore.Common.Dtos;

namespace MinecraftSpelunking.Application.AspNetCore.Common.Services
{
    public interface IRedirectApplicationService
    {
        void RedirectTo(string uri, Dictionary<string, object?>? queryParameters = null, StatusMessageDto? statusMessage = null);
    }
}
