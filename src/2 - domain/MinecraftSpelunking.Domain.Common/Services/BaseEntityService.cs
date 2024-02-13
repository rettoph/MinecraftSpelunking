using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Common.Services;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Database.Common;

namespace MinecraftSpelunking.Domain.Common.Services
{
    public abstract class BaseEntityService<TEntity> : MappingService<TEntity>, IBaseEntityService<TEntity>
        where TEntity : BaseEntity
    {
        public BaseEntityService(Func<IDataContext, DbSet<TEntity>> entities, DataContext context, IMapper mapper) : base(entities, context, mapper)
        {
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await this.entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return this.entities.AsQueryable();
        }

        public async Task AddAsync(TEntity entity, bool saveChanges = true)
        {
            this.entities.Add(entity);

            if (saveChanges)
            {
                await this.context.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(TEntity entity, bool saveChanges = true)
        {
            this.entities.Remove(entity);

            if (saveChanges)
            {
                await this.context.SaveChangesAsync();
            }
        }
    }
}
