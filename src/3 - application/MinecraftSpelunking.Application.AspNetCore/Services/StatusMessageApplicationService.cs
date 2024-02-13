using MinecraftSpelunking.Application.AspNetCore.Common.Dtos;
using MinecraftSpelunking.Application.AspNetCore.Common.Extensions;
using MinecraftSpelunking.Application.AspNetCore.Common.Services;

namespace MinecraftSpelunking.Application.AspNetCore.Services
{
    internal sealed class StatusMessageApplicationService : IStatusMessageApplicationService
    {
        private readonly ICookieApplicationService _cookies;
        private readonly List<StatusMessageDto> _statusMessages;

        public event EventHandler<StatusMessageDto>? OnStatusMessageAdded;

        public StatusMessageApplicationService(ICookieApplicationService cookies)
        {
            _cookies = cookies;
            _statusMessages = new List<StatusMessageDto>();

            if (_cookies.TryReadStatusMessage(out StatusMessageDto? cookieMessage))
            {
                _statusMessages.Add(cookieMessage);
            }
        }

        public void Add(StatusMessageDto statusMessage, bool asCookie = false)
        {
            _statusMessages.Add(statusMessage);
            this.OnStatusMessageAdded?.Invoke(this, statusMessage);
        }

        public IEnumerable<StatusMessageDto> GetAll()
        {
            return _statusMessages;
        }
    }
}
