using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Common.Services;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Identity.Common.Services;

namespace MinecraftSpelunking.Domain.Identity.Services
{
    internal sealed class UserService : MappingService<User>, IUserService
    {
        public UserService(DataContext context, IMapper mapper) : base(x => x.Users, context, mapper)
        {
        }

        public bool Any()
        {
            return this.entities.Any();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await this.entities.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await this.entities.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
