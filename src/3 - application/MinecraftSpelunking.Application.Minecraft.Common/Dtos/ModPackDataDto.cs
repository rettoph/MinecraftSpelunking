﻿namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record ModPackDataDto
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        public int VersionId { get; set; }

        public bool IsMetadata { get; set; }
    }
}
