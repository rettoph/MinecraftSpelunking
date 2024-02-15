using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Common;
using MinecraftSpelunking.Common.Services;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Database.Common;

namespace MinecraftSpelunking.Domain.Common.Services
{
    public abstract class MappingService<TEntity> : IMappingService<TEntity>
        where TEntity : class
    {
        protected readonly IMapper mapper;
        protected readonly DataContext context;
        protected readonly DbSet<TEntity> entities;

        public MappingService(Func<IDataContext, DbSet<TEntity>> entities, DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
            this.entities = entities(context);
        }

        public TEntity Map(object? dto)
        {
            return this.mapper.Map<TEntity>(dto);
        }

        public TDto Map<TDto>(TEntity? entity)
        {
            return this.mapper.Map<TDto>(entity);
        }

        public IEnumerable<TDto> Map<TDto>(IEnumerable<TEntity> entities)
        {
            return this.mapper.Map<IEnumerable<TDto>>(entities);
        }

        public Page<TDto> Map<TDto>(Page<TEntity> entities)
        {
            return this.mapper.Map<Page<TDto>>(entities);
        }

        public IPersistence<TEntity> Persist()
        {
            return this.entities.Persist(this.mapper);
        }
    }
}
