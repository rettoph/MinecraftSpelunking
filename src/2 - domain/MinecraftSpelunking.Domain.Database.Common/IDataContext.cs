using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Database.Common
{
    public interface IDataContext
    {
        /* Identity Entities */
        DbSet<User> Users { get; }

        /* Minecraft Entities */
        DbSet<ReservedAddressBlock> ReservedAddressBlocks { get; set; }
        DbSet<AddressBlock> AddressBlocks { get; set; }
        DbSet<AddressBlockAssignment> AddressBlockAssignments { get; set; }
        DbSet<JavaServer> JavaServers { get; set; }
        DbSet<ServerIcon> ServerIcons { get; set; }
        DbSet<ModType> ModTypes { get; set; }
        DbSet<Mod> Mods { get; set; }
        DbSet<ModVersion> ModVersions { get; set; }
        DbSet<ModPackData> ModPackData { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
