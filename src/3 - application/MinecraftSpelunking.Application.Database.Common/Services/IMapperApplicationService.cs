using AutoMapper.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Database.Common;

namespace MinecraftSpelunking.Application.Database.Common.Services
{
    public interface IMapperApplicationService
    {
        IPersistence<TEntity> Persist<TEntity>(Func<IDataContext, DbSet<TEntity>> entities)
            where TEntity : class;

        TTo Map<TTo>(object from)
            where TTo : class;
    }
}
