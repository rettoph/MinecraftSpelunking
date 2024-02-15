using Microsoft.AspNetCore.Mvc;
using MinecraftSpelunking.Application.Database.Common.Services;
using MinecraftSpelunking.Application.Identity.Common.Attributes;
using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Application.Identity.Common.Services;
using MinecraftSpelunking.Application.Minecraft.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Common.Services;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using MinecraftSpelunking.Presentation.Common;
using MinecraftSpelunking.Presentation.Common.Models;

namespace MinecraftSpelunking.Presentation.WebServer.Controllers.Api.v1
{
    [ApiController]
    public class AddressBlockAssignmentController : MinecraftSpelunkingBaseController
    {
        private readonly IMapperApplicationService _mapper;
        private readonly IUserApplicationService _users;
        private readonly IAddressBlockAssignmentApplicationService _addressBlocks;

        public AddressBlockAssignmentController(IAddressBlockAssignmentApplicationService addressBlocks, IUserApplicationService users, IMapperApplicationService mapper)
        {
            _users = users;
            _addressBlocks = addressBlocks;
            _mapper = mapper;
        }

        [HttpGet]
        [BasicAuthorization]
        [Route(Constants.Routes.Api.v1.AddressBlockAssignment.Get)]
        public async Task<Response<AddressBlockAssignmentsModel>> Get([FromQuery] int count)
        {
            if (count < 1)
            {
                return this.Response(default(AddressBlockAssignmentsModel), StatusCodes.Status422UnprocessableEntity, $"{nameof(count)} must be greater than 0.");
            }

            if (count > 256)
            {
                return this.Response(default(AddressBlockAssignmentsModel), StatusCodes.Status422UnprocessableEntity, $"{nameof(count)} must be less than 257.");
            }

            UserDto? userDto = await _users.GetCurrentUserAsync();
            if (userDto is null)
            {
                return this.Response(default(AddressBlockAssignmentsModel), StatusCodes.Status404NotFound, $"{nameof(User)} not found.");
            }

            AddressBlockAssignmentDto[] assignments = await _addressBlocks.GetAssignmentsAsync(userDto, count);
            if (assignments.Length == 0)
            {
                return this.Response(default(AddressBlockAssignmentsModel), StatusCodes.Status404NotFound, $"{nameof(AddressBlockAssignment)} not found.");
            }

            AddressBlockAssignmentModel[] assignmentModel = _mapper.Map<AddressBlockAssignmentModel[]>(assignments);
            return this.Response(new AddressBlockAssignmentsModel()
            {
                Assignments = _mapper.Map<AddressBlockAssignmentModel[]>(assignments)
            });
        }

        [HttpPost]
        [BasicAuthorization]
        [Route("complete")]
        public async Task<Response<AddressBlockAssignmentModel>> Complete([FromBody] AddressBlockAssignmentResultsModel results)
        {
            UserDto? userDto = await _users.GetCurrentUserAsync();
            if (userDto is null)
            {
                return this.Response(default(AddressBlockAssignmentModel), StatusCodes.Status404NotFound, $"{nameof(User)} not found.");
            }

            ServerDto[] javaServers = _mapper.Map<ServerDto[]>(results.JavaServers);

            bool completeAssignmentResult = await _addressBlocks.TryCompleteAssignmentsAsync(results.Id, userDto, javaServers);
            if (completeAssignmentResult == false)
            {
                return this.Response(default(AddressBlockAssignmentModel), StatusCodes.Status500InternalServerError, $"Unknown error completing assignment.");
            }

            AddressBlockAssignmentDto[] assignments = await _addressBlocks.GetAssignmentsAsync(userDto, 1);
            if (assignments.Length == 0)
            {
                return this.Response(default(AddressBlockAssignmentModel), StatusCodes.Status404NotFound, $"{nameof(AddressBlockAssignment)} not found.");
            }

            AddressBlockAssignmentModel newAssignmentModel = _mapper.Map<AddressBlockAssignmentModel>(assignments[0]);
            return this.Response(newAssignmentModel);
        }
    }
}
