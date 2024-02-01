using Microsoft.AspNetCore.Mvc;

namespace MinecraftSpelunking.Presentation.WebServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
