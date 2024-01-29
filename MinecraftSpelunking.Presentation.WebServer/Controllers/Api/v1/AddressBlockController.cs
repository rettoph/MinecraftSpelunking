using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinecraftSpelunking.Application.Account.Services;
using MinecraftSpelunking.Application.Minecraft.Dtos;
using MinecraftSpelunking.Application.Minecraft.Services;
using MinecraftSpelunking.Common.Account;
using MinecraftSpelunking.Common.Minecraft.Services;
using MinecraftSpelunking.Presentation.WebServer.Attributes;
using MinecraftSpelunking.Presentation.WebServer.Models;

namespace MinecraftSpelunking.Presentation.WebServer.Controllers.Api.v1
{
    [Route("/api/v1/address-block")]
    [AccessToken(UserRoleTypeEnum.User)]
    public class AddressBlockController : Controller
    {
        private readonly IAddressBlockApplicationService _addressBlocks;
        private readonly IAccountApplicationService _accounts;
        private readonly IJavaServerService _javaServers;
        private readonly IMapper _mapper;

        public AddressBlockController(
            IAddressBlockApplicationService addressBlocks,
            IAccountApplicationService accounts,
            IJavaServerService javaServers,
            IMapper mapper)
        {
            _addressBlocks = addressBlocks;
            _accounts = accounts;
            _javaServers = javaServers;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("get-assignment")]
        public IActionResult GetAssignment([FromBody] AddressBlockResultModel? results)
        {
            if (results is not null)
            {
                ServerDto[] javaServers = _mapper.Map<ServerDto[]>(results.JavaServers);
                _addressBlocks.Complete(results.Id, javaServers);
            }

            AddressBlockDto block = _addressBlocks.GetAssignment(_accounts.TryGetCurrentUser()!);

            return Ok(block);
        }
    }
}
