using MinecraftSpelunking.Presentation.Client.Models;
using System.Collections.Specialized;

namespace MinecraftSpelunking.Presentation.Client.Services
{
    public interface IMinecraftSpelunkingClientService : IDisposable
    {
        Task<HttpResponse<TResponse>> PostAsync<TBody, TResponse>(string path, TBody body, NameValueCollection? query = null);
    }
}
