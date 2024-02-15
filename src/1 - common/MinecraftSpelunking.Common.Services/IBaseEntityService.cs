using MinecraftSpelunking.Common.Database;

namespace MinecraftSpelunking.Common.Services
{
    public interface IBaseEntityService<TEntity> : IMappingService<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> AsQueryable();
    }
}
