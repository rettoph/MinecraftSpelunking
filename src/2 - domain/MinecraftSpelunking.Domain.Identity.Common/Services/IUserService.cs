using MinecraftSpelunking.Common.Services;
using MinecraftSpelunking.Domain.Identity.Common.Entities;

namespace MinecraftSpelunking.Domain.Identity.Common.Services
{
    public interface IUserService : IMappingService<User>
    {
        /// <summary>
        /// Determin wether or not any users have been created.
        /// </summary>
        /// <returns></returns>
        bool Any();

        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
    }
}
