using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MinecraftSpelunking.Domain.Database.Services
{
    public sealed class EntityMapperService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EntityMapperService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TEntity MapToEntity<TEntity, TDto>(TDto dto, Func<DataContext, DbSet<TEntity>> selector)
            where TEntity : class
            where TDto : class
        {
            return selector(_context).Persist(_mapper).InsertOrUpdate(dto);
        }

        public TDto MapToDto<TDto>(object entity)
            where TDto : class
        {
            return _mapper.Map<TDto>(entity);
        }
    }
}
