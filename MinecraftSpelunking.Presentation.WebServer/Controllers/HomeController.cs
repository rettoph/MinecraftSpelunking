using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinecraftSpelunking.Application.Account.Services;
using MinecraftSpelunking.Presentation.WebServer.Models;
using System.Diagnostics;

namespace MinecraftSpelunking.Presentation.WebServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountApplicationService _accounts;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IAccountApplicationService accounts,
            ILogger<HomeController> logger)
        {
            _accounts = accounts;
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_accounts.TryGetCurrentUser()!);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
