using MinecraftSpelunking.Common.Minecraft.Enums;
using System.Net;

namespace MinecraftSpelunking.Common.Minecraft.Entities
{
    public class AddressBlock : BaseEntity
    {
        public const byte CIDR = 22;

        public IPNetwork2 Network { get; set; } = default!;

        public List<AddressBlockAssignment> Assignments { get; set; } = [];

        public AddressBlockStatusEnum Status { get; set; } = AddressBlockStatusEnum.Available;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}
