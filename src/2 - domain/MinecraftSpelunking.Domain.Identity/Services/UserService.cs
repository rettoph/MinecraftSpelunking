using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Database.Common;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Identity.Common.Services;

namespace MinecraftSpelunking.Domain.Identity.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IDataContext _context;

        public UserService(IDataContext context)
        {
            _context = context;
        }

        public bool Any()
        {
            return _context.Users.Any();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
