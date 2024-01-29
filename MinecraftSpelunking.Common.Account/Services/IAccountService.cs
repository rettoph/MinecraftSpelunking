using MinecraftSpelunking.Common.Account.Entities;

namespace MinecraftSpelunking.Common.Account.Services
{
    public interface IAccountService
    {
        User? TryGetUserById(int userId);

        Task<User?> TryGetUserByEmailAsync(string email);

        Task SignOutAsync();
        Task<SignInAttemptResultEnum> TrySignInWithEmailAndPasswordAsync(string email, string password);
        Task<User?> TrySignInWithApiAccessToken(string apiAccessToken, params UserRoleTypeEnum[] roles);

        bool AtLeastOneUserExists();

        User Create(string email, string password, params UserRoleTypeEnum[] roles);
    }
}
