using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinecraftSpelunking.Application.Account.Services;
using MinecraftSpelunking.Application.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Dtos;
using MinecraftSpelunking.Application.Minecraft.Services;
using MinecraftSpelunking.Presentation.WebServer.Models;

namespace MinecraftSpelunking.Presentation.WebServer.Controllers
{
    public class JavaServerController : BaseController
    {
        private readonly IJavaServerApplicationService _javaServers;
        private readonly IAddressBlockApplicationService _addressBlocks;
        private readonly IMapper _mapper;

        public JavaServerController(
            IAccountApplicationService accounts,
            IJavaServerApplicationService javaServers,
            IAddressBlockApplicationService addressBlocks,
            IMapper mapper) : base(accounts)
        {
            _javaServers = javaServers;
            _addressBlocks = addressBlocks;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            PageDto<JavaServerDto> pageDto = await _javaServers.GetAllAsync(0);
            PageModel<JavaServerModel> pageModel = _mapper.Map<PageModel<JavaServerModel>>(pageDto);

            return View(pageModel);
        }

        public async Task<IActionResult> View(int id)
        {
            JavaServerDto serverDto = await _javaServers.GetByIdAsync(id);
            JavaServerModel javaServerModel = _mapper.Map<JavaServerModel>(serverDto);

            return View(javaServerModel);
        }

        public async Task<IActionResult> Refresh(int id)
        {
            await _javaServers.RefreshAsync(id);

            return RedirectToAction(nameof(Index), "JavaServer");
        }
    }
}
