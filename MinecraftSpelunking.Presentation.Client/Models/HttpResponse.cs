using System.Net;

namespace MinecraftSpelunking.Presentation.Client.Models
{
    public class HttpResponse<TContent>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public TContent? Content { get; set; }
    }
}
