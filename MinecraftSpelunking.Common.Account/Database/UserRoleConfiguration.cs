using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Common.Account.Entities;

namespace MinecraftSpelunking.Common.Account.Database
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
                    Name = type.ToString(),
                    NormalizedName = type.ToString(),
                    Type = type
                });
            }
        }
    }
}
