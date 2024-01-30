using Microsoft.AspNetCore.Mvc;
using MinecraftSpelunking.Application.Account.Services;
using MinecraftSpelunking.Presentation.WebServer.Models;
using System.Diagnostics;

namespace MinecraftSpelunking.Presentation.WebServer.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IAccountApplicationService accounts,
            ILogger<HomeController> logger) : base(accounts)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Index), "JavaServer");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
