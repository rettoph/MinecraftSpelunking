using Microsoft.AspNetCore.Authorization;
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
            if (_accounts.AtLeastOneUserExists() == false)
            {
                return RedirectToAction(nameof(CreateAdminAccount), "Account");
            }

            await _accounts.TrySignOutAsync();

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
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accounts.TrySignOutAsync();

            string returnUrl = Url.Content("~/");
            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public Task<IActionResult> CreateAdminAccount()
        {
            return Task.FromResult<IActionResult>(View());
        }

        [HttpPost]
        public Task<IActionResult> CreateAdminAccount(CreateAdminAccountRequestModel request)
        {
            if (request.Password != request.RePassword)
            {
                return Task.FromResult<IActionResult>(View(new CreateAdminAccounResponseModel()
                {
                    Email = request.Email,
                    Message = "Passwords to not match"
                }));
            }

            _accounts.Create(request.Email, request.Password, UserRoleTypeEnum.User, UserRoleTypeEnum.Admin);

            string returnUrl = Url.Content("~/");
            return Task.FromResult<IActionResult>(LocalRedirect(returnUrl));
        }
    }
}
