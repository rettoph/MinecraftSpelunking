using MinecraftSpelunking.Common.Account.Entities;

namespace MinecraftSpelunking.Common.Account.Services
{
    public interface IAccountService
    {
        User? TryGetUserById(int userId);
        User? TryGetUserByApiAccessToken(string apiAccessToken);

        Task<User?> TryGetUserByEmailAsync(string email);

        Task SignOutAsync();
        Task<SignInAttemptResultEnum> TrySignInWithEmailAndPasswordAsync(string email, string password);

        bool VerifyUserHasAllRoles(int userId, params UserRoleTypeEnum[] roles);
    }
}
