using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinecraftSpelunking.Application.Minecraft.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Common.Services;
using MinecraftSpelunking.Common;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Services;

namespace MinecraftSpelunking.Application.Minecraft.Services
{
    internal sealed class JavaServerApplicationService : IJavaServerApplicicationService
    {
        private readonly IJavaServerService _javaServers;

        public JavaServerApplicationService(IJavaServerService javaServers)
        {
            _javaServers = javaServers;
        }

        public async Task<JavaServerDto?> GetJavaServerAsync(int id)
        {
            JavaServer? javaServer = await _javaServers.AsQueryable()
                .Include(x => x.Icon)
                .Include(x => x.ModPackData)
                .Include(x => x.ModVersions)
                .ThenInclude(x => x.Mod)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (javaServer is not null)
            {
                await _javaServers.RefreshAsync(new Server()
                {
                    Host = javaServer.Host,
                    Port = javaServer.Port,
                });
            }


            JavaServerDto? javaServerDto = _javaServers.Map<JavaServerDto>(javaServer);

            return javaServerDto;
        }

        public Task<Page<JavaServerDto>> GetJavaServersAsync(int number, int size, string query)
        {
            IQueryable<JavaServer> queryable = _javaServers.AsQueryable().Include(x => x.Icon);
            if (query.IsNullOrEmpty() == false)
            {
                queryable = queryable.Where(x => x.DescriptionNormalized.Contains(query) || x.Host.Contains(query));
            }

            Page<JavaServer> page = queryable.Page(number, size);
            Page<JavaServerDto> pageDto = _javaServers.Map<JavaServerDto>(page);

            return Task.FromResult(pageDto);
        }
    }
}
