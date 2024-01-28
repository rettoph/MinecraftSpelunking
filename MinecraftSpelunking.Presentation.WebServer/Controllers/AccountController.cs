using Microsoft.AspNetCore.Mvc;
using MinecraftSpelunking.Application.Account.Models;
using MinecraftSpelunking.Application.Account.Services;
using MinecraftSpelunking.Common.Account;
using MinecraftSpelunking.Presentation.WebServer.Models;

namespace MinecraftSpelunking.Presentation.WebServer.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountApplicationService _accounts;

        public AccountController(IAccountApplicationService accounts)
        {
            _accounts = accounts;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var test = await _accounts.TrySignOutAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel input)
        {
            string returnUrl = Url.Content("~/");

            SignInAttemptResult result = await _accounts.TrySignInWithEmailAndPasswordAsync(input.Email, input.Password);

            if (result.Result == SignInAttemptResultEnum.Success)
            {
                return LocalRedirect(returnUrl);
            }

            return View(new LoginResponseModel()
            {
                Message = "There was an error logging in, please try again.",
                Email = input.Email
            });
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accounts.TrySignOutAsync();

            string returnUrl = Url.Content("~/");
            return LocalRedirect(returnUrl);
        }

    }
}
