using Microsoft.AspNetCore.Mvc;
using MinecraftSpelunking.Application.Account.Services;
using MinecraftSpelunking.Common.Account.Entities;

namespace MinecraftSpelunking.Presentation.WebServer.Controllers
{
    public abstract class BaseController : Controller
    {
        public readonly IAccountApplicationService Accounts;
        public new readonly User? User;

        public BaseController(IAccountApplicationService accounts)
        {
            this.Accounts = accounts;
            this.User = this.Accounts.TryGetCurrentUser();
        }
    }
}
