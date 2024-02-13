using AutoMapper.EntityFrameworkCore;

namespace MinecraftSpelunking.Common.Services
{
    public interface IMappingService<TEntity>
        where TEntity : class
    {
        IPersistence<TEntity> Persist();
        TEntity Map(object? dto);
        TDto Map<TDto>(TEntity? entity);
    }
}
