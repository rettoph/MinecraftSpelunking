using MinecraftSpelunking.Application.Database.Common.Services;
using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Common.Services;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Identity.Common.Services;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Services;

namespace MinecraftSpelunking.Application.Minecraft.Services
{
    internal sealed class AddressBlockApplicationService : IAddressBlockApplicationService
    {
        private readonly IMapperApplicationService _mapper;
        private readonly IUserService _users;
        private readonly IAddressBlockService _addressBlocks;

        public AddressBlockApplicationService(IUserService users, IAddressBlockService addressBlocks, IMapperApplicationService mapper)
        {
            _users = users;
            _addressBlocks = addressBlocks;
            _mapper = mapper;
        }

        public async Task<AddressBlockAssignmentDto?> GetAssignmentAsync(UserDto userDto)
        {
            AddressBlock? block = await _addressBlocks.GetAssignableAddressBlockAsync();
            if (block is null)
            {
                return null;
            }

            User user = _users.Persist().InsertOrUpdate(userDto);
            AddressBlockAssignment? assignment = await _addressBlocks.TryAssignAddressBlockAsync(block, user);
            if (assignment is null)
            {
                return null;
            }

            return _mapper.Map<AddressBlockAssignmentDto>(assignment);
        }
    }
}
