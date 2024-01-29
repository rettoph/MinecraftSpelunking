﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using MinecraftSpelunking.Common.Account;
using MinecraftSpelunking.Common.Account.Entities;
using MinecraftSpelunking.Common.Account.Services;
using MinecraftSpelunking.Domain.Database;

namespace MinecraftSpelunking.Domain.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly DataContext _dataContext;

        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager, DataContext dataContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dataContext = dataContext;
        }

        public Task<User?> TryGetUserByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public User TryGetUserById(int userId)
        {
            return _dataContext.Users.First(x => x.Id == userId);
        }

        public async Task<SignInAttemptResultEnum> TrySignInWithEmailAndPasswordAsync(string email, string password)
        {
            User? user = await this.TryGetUserByEmailAsync(email);
            if (user is null)
            {
                return SignInAttemptResultEnum.Failure;
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, password, true, false);

            if (result.Succeeded)
            {
                return SignInAttemptResultEnum.Success;
            }

            return SignInAttemptResultEnum.Failure;
        }

        public async Task<User?> TrySignInWithApiAccessToken(string apiAccessToken, params UserRoleTypeEnum[] roles)
        {
            User? user = _dataContext.Users.FirstOrDefault(x => x.ApiAccessToken == apiAccessToken);
            if (user is null)
            {
                return user;
            }

            List<IdentityUserRole<int>> userRoles = _dataContext.UserRoles.Where(x => x.UserId == user.Id).ToList();

            foreach (UserRoleTypeEnum role in roles)
            {
                if (userRoles.Any(x => x.RoleId == (int)role) == false)
                {
                    return null;
                }
            }

            await _signInManager.SignInAsync(user, new AuthenticationProperties()
            {
                AllowRefresh = false,
                IsPersistent = false
            });

            return user;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
