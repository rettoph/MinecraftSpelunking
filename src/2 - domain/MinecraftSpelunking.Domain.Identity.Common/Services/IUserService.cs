using MinecraftSpelunking.Domain.Identity.Common.Entities;

namespace MinecraftSpelunking.Domain.Identity.Common.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Determin wether or not any users have been created.
        /// </summary>
        /// <returns></returns>
        bool Any();

        Task<User?> GetUserByEmailAsync(string email);
    }
}
