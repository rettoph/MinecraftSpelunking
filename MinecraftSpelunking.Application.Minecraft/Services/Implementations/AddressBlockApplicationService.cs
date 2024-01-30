using AutoMapper;
using MinecraftSpelunking.Application.Minecraft.Dtos;
using MinecraftSpelunking.Common.Account.Entities;
using MinecraftSpelunking.Common.Minecraft.Entities;
using MinecraftSpelunking.Common.Minecraft.Services;

namespace MinecraftSpelunking.Application.Minecraft.Services.Implementations
{
    public sealed class AddressBlockApplicationService : IAddressBlockApplicationService
    {
        private readonly IAddressBlockService _addressBlocks;
        private readonly IMapper _mapper;

        public AddressBlockApplicationService(
            IAddressBlockService addressBlocks,
            IMapper mapper)
        {
            _addressBlocks = addressBlocks;
            _mapper = mapper;
        }

        public void Complete(int id, ServerDto[] javaDiscoveriesDto)
        {
            Server[] javaDiscoveries = _mapper.Map<Server[]>(javaDiscoveriesDto);
            _addressBlocks.Complete(id, javaDiscoveries);
        }

        public AddressBlockDto? GetAssignment(User user)
        {
            AddressBlock? block = _addressBlocks.GetAssignment(user);
            AddressBlockDto? blockDto = _mapper.Map<AddressBlockDto?>(block);

            return blockDto;
        }
    }
}
