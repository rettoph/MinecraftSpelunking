using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Identity.Common.Entities;

namespace MinecraftSpelunking.Domain.Database.Common
{
    public interface IDataContext
    {
        DbSet<User> Users { get; }
    }
}
