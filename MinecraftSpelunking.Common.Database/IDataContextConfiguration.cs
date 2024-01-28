using Microsoft.EntityFrameworkCore;

namespace MinecraftSpelunking.Common.Database
{
    public interface IDataContextConfiguration
    {
        static abstract void OnModelCreating(ModelBuilder builder);
    }
}
