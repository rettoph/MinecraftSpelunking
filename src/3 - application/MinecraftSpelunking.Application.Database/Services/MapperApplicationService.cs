using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Application.Database.Common.Services;
using MinecraftSpelunking.Domain.Database.Common;

namespace MinecraftSpelunking.Application.Database.Services
{
    internal sealed class MapperApplicationService : IMapperApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IDataContext _context;

        public MapperApplicationService(IDataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public IPersistence<TEntity> Persist<TEntity>(Func<IDataContext, DbSet<TEntity>> entities)
            where TEntity : class
        {
            return entities(_context).Persist(_mapper);
        }

        public TTo Map<TTo>(object from)
            where TTo : class
        {
            return _mapper.Map<TTo>(from);
        }
    }
}
