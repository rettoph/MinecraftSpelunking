using Microsoft.AspNetCore.Mvc;
using MinecraftSpelunking.Application.Database.Common.Services;
using MinecraftSpelunking.Application.Identity.Common.Attributes;
using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Application.Identity.Common.Services;
using MinecraftSpelunking.Application.Minecraft.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Common.Services;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using MinecraftSpelunking.Presentation.WebServer.Models;

namespace MinecraftSpelunking.Presentation.WebServer.Controllers.Api.v1
{
    [Route("/api/v1/address-blocks/")]
    [ApiController]
    public class AddressBlocksController : MinecraftSpelunkingBaseController
    {
        private readonly IMapperApplicationService _mapper;
        private readonly IUserApplicationService _users;
        private readonly IAddressBlockApplicationService _addressBlocks;

        public AddressBlocksController(IAddressBlockApplicationService addressBlocks, IUserApplicationService users, IMapperApplicationService mapper)
        {
            _users = users;
            _addressBlocks = addressBlocks;
            _mapper = mapper;
        }

        [HttpGet]
        [BasicAuthorization]
        [Route("assign")]
        public async Task<Response<AddressBlockAssignmentModel>> Assign()
        {
            UserDto? user = await _users.GetCurrentUserAsync();
            if (user is null)
            {
                return this.Response(default(AddressBlockAssignmentModel), StatusCodes.Status404NotFound, $"{nameof(User)} not found.");
            }

            AddressBlockAssignmentDto? assignment = await _addressBlocks.GetAssignmentAsync(user);
            if (assignment is null)
            {
                return this.Response(default(AddressBlockAssignmentModel), StatusCodes.Status404NotFound, $"{nameof(AddressBlockAssignment)} not found.");
            }

            AddressBlockAssignmentModel assignmentModel = _mapper.Map<AddressBlockAssignmentModel>(assignment);
            return this.Response(assignmentModel);
        }
    }
}
