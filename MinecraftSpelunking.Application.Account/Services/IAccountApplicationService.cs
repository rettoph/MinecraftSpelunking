﻿using MinecraftSpelunking.Application.Account.Models;
using MinecraftSpelunking.Common.Account;
using MinecraftSpelunking.Common.Account.Entities;

namespace MinecraftSpelunking.Application.Account.Services
{
    public interface IAccountApplicationService
    {
        User? TryGetUserById(int userId);

        User? TryGetCurrentUser();
        Task<bool> TrySignOutAsync();
        Task<SignInAttemptResult> TrySignInWithEmailAndPasswordAsync(string email, string password);
        Task<User?> TrySignInWithApiAccessToken(string apiAccessToken, params UserRoleTypeEnum[] roles);
    }
}
