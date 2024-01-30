using MinecraftSpelunker.Application.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Dtos;
using MinecraftSpelunking.Common.Minecraft.Entities;
using MinecraftSpelunking.Common.Minecraft.Services;
using MinecraftSpelunking.Domain.Database.Services;

namespace MinecraftSpelunking.Application.Minecraft.Services.Implementations
{
    public sealed class JavaServerApplicationService : IJavaServerApplicationService
    {
        private readonly IJavaServerService _javaServers;
        private readonly EntityMapperService _mapper;

        public JavaServerApplicationService(IJavaServerService javaServers, EntityMapperService mapper)
        {
            _javaServers = javaServers;
            _mapper = mapper;
        }

        public async Task<JavaServerDto> AddAsync(string host, int port, AddressBlockDto blockDto)
        {
            AddressBlock block = _mapper.MapToEntity(blockDto, ctx => ctx.AddressBlocks);

            JavaServer server = await _javaServers.AddAsync(host, port, block);
            JavaServerDto serverDto = _mapper.MapToDto<JavaServerDto>(server);

            return serverDto;
        }

        public async Task<JavaServerDto> RefreshAsync(int id)
        {
            JavaServer server = await _javaServers.RefreshAsync(id);
            JavaServerDto serverDto = _mapper.MapToDto<JavaServerDto>(server);

            return serverDto;
        }

        public async Task<JavaServerDto> GetByIdAsync(int id)
        {
            JavaServer server = await _javaServers.GetByIdAsync(id);
            JavaServerDto serverDto = _mapper.MapToDto<JavaServerDto>(server);

            return serverDto;
        }

        public async Task<PageDto<JavaServerDto>> GetAllAsync(int page, int size = 100)
        {
            JavaServer[] javaServers = await _javaServers.GetAllAsync(size, page * size);
            JavaServerDto[] javaServerDtos = _mapper.MapToDto<JavaServerDto[]>(javaServers);

            return new PageDto<JavaServerDto>()
            {
                Number = page,
                Size = size,
                Items = javaServerDtos
            };
        }
    }
}
