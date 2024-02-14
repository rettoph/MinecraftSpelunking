using MinecraftSpelunking.Domain.Minecraft.Common.Enums;

namespace MinecraftSpelunking.Presentation.WebServer.Models
{
    public class AddressBlockModel
    {
        public int Id { get; set; }

        public IPNetwork2Model Network { get; set; } = default!;

        public AddressBlockStatusEnum Status { get; set; }
    }
}
