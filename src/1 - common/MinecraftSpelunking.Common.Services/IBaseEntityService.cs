using MinecraftSpelunking.Common.Database;

namespace MinecraftSpelunking.Common.Services
{
    public interface IBaseEntityService<TEntity> : IMappingService<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> AsQueryable();

        Task<TEntity?> GetByIdAsync(int id);

        Task AddAsync(TEntity entity, bool saveChanges = true);
        Task RemoveAsync(TEntity entity, bool saveChanges = true);
    }
}
