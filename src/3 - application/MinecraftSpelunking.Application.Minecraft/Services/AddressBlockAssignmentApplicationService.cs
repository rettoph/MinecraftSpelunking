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
    internal sealed class AddressBlockAssignmentApplicationService : IAddressBlockAssignmentApplicationService
    {
        private readonly IMapperApplicationService _mapper;
        private readonly IUserService _users;
        private readonly IAddressBlockAssignmentService _addressBlockAssignments;

        public AddressBlockAssignmentApplicationService(IUserService users, IAddressBlockAssignmentService addressBlockAssignments, IMapperApplicationService mapper)
        {
            _users = users;
            _addressBlockAssignments = addressBlockAssignments;
            _mapper = mapper;
        }

        public async Task<bool> TryCompleteAssignmentsAsync(int id, UserDto userDto, ServerDto[] javaServerDtos)
        {
            User user = _users.Map(userDto);
            Server[] javaServers = _mapper.Map<Server[]>(javaServerDtos);

            return await _addressBlockAssignments.TryCompleteAssignmentsAsync(id, user, javaServers);
        }

        public async Task<AddressBlockAssignmentDto[]> GetAssignmentsAsync(UserDto userDto, int count)
        {
            User user = _users.Persist().InsertOrUpdate(userDto);
            AddressBlockAssignment[] assignments = await _addressBlockAssignments.GetAssignmentsAsync(user, count);

            return _mapper.Map<AddressBlockAssignmentDto[]>(assignments);
        }
    }
}
