using Microsoft.AspNetCore.Hosting;
using MinecraftSpelunking.Common.Minecraft.Entities;
using MinecraftSpelunking.Common.Minecraft.Services;
using MinecraftSpelunking.Domain.Database;
using Standart.Hash.xxHash;
using System.Runtime.CompilerServices;

namespace MinecraftSpelunking.Domain.Minecraft.Services
{
    public sealed class ServerIconService : IServerIconService
    {
        private const string Base64PngPrefix = "data:image/png;base64,";
        private const string IconPath = "icons";

        private readonly DataContext _context;
        private readonly IWebHostEnvironment _hostingEnv;

        public ServerIconService(DataContext context, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        public int GetBase64Hash(string base64)
        {
            uint hash = xxHash32.ComputeHash(base64);
            return Unsafe.As<uint, int>(ref hash);
        }

        public ServerIcon Add(string base64)
        {
            string dir = Path.Combine(_hostingEnv.WebRootPath, IconPath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string name = Guid.NewGuid().ToString() + ".png";
            string path = Path.Combine(dir, name);

            string trimmedBase64 = base64.StartsWith(Base64PngPrefix) ? base64[Base64PngPrefix.Length..] : base64;
            File.WriteAllBytes(path, Convert.FromBase64String(trimmedBase64));


            ServerIcon instance = new ServerIcon()
            {
                Hash = this.GetBase64Hash(base64),
                Path = "/" + Path.Combine(IconPath, name).Replace("\\", "/")
            };

            _context.Add(instance);
            _context.SaveChanges();

            return instance;
        }
    }
}
