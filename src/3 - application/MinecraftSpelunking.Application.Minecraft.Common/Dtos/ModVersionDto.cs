namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record ModVersionDto
    {
        public static readonly ModVersionDto Empty = new ModVersionDto();

        public int Id { get; set; }
        public int ModId { get; set; }
        public ModDto Mod { get; set; } = ModDto.Empty;
        public string Version { get; set; } = string.Empty;

        public List<JavaServerDto> JavaServers { get; set; } = [];
    }
}
