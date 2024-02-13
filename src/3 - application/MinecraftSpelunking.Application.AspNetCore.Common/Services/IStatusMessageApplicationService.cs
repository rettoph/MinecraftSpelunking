using MinecraftSpelunking.Application.AspNetCore.Common.Dtos;

namespace MinecraftSpelunking.Application.AspNetCore.Common.Services
{
    public interface IStatusMessageApplicationService
    {
        event EventHandler<StatusMessageDto>? OnStatusMessageAdded;

        void Add(StatusMessageDto statusMessage, bool asCookie = false);

        IEnumerable<StatusMessageDto> GetAll();
    }
}
