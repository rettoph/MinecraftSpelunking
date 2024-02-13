using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Identity.Common.Enums;

namespace MinecraftSpelunking.Domain.Identity.Common.Database
{
    internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            foreach (UserRoleTypeEnum type in Enum.GetValues<UserRoleTypeEnum>())
            {
                builder.HasData(new UserRole()
                {
                    Id = (int)type,
                    Name = UserRole.GetName(type),
                    NormalizedName = UserRole.GetName(type),
                    Type = type
                });
            }
        }
    }
}
