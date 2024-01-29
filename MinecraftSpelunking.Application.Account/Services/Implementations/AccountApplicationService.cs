using Microsoft.AspNetCore.Http;
using MinecraftSpelunking.Application.Account.Models;
using MinecraftSpelunking.Common.Account;
using MinecraftSpelunking.Common.Account.Entities;
using MinecraftSpelunking.Common.Account.Services;
using System.Security.Claims;

namespace MinecraftSpelunking.Application.Account.Services.Implementations
{
    public sealed class AccountApplicationService : IAccountApplicationService
    {
        private readonly IAccountService _accounts;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountApplicationService(IAccountService accounts, IHttpContextAccessor httpContextAccessor)
        {
            _accounts = accounts;
            _httpContextAccessor = httpContextAccessor;
        }

        public User? TryGetUserById(int userId)
        {
            return _accounts.TryGetUserById(userId);
        }

        public async Task<bool> TrySignOutAsync()
        {
            await _accounts.SignOutAsync();

            return true;
        }

        public async Task<SignInAttemptResult> TrySignInWithEmailAndPasswordAsync(string email, string password)
        {
            SignInAttemptResultEnum result = await _accounts.TrySignInWithEmailAndPasswordAsync(email, password);

            if (result == SignInAttemptResultEnum.Failure)
            {
                return SignInAttemptResult.Failure();
            }

            User? user = await _accounts.TryGetUserByEmailAsync(email);
            if (user is null)
            {
                return SignInAttemptResult.Failure();
            }

            return SignInAttemptResult.Success(user);
        }

        public async Task<User?> TrySignInWithApiAccessToken(string apiAccessToken, params UserRoleTypeEnum[] roles)
        {
            return await _accounts.TrySignInWithApiAccessToken(apiAccessToken, roles);
        }

        public User? TryGetCurrentUser()
        {
            Claim? userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null || int.TryParse(userIdClaim.Value, out int userId) == false)
            {
                return null;
            }

            return _accounts.TryGetUserById(userId);
        }

        public bool AtLeastOneUserExists()
        {
            return _accounts.AtLeastOneUserExists();
        }

        public User Create(string email, string password, params UserRoleTypeEnum[] roles)
        {
            return _accounts.Create(email, password, roles);
        }
    }
}
