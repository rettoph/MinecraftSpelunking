using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Database.Converters
{
    public sealed class IpNetwork2Converter : ValueConverter<IPNetwork2, string>
    {
        public IpNetwork2Converter() : base(v => v.ToString(), v => IPNetwork2.Parse(v))
        {
        }

        public static void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<IPNetwork2>().HaveConversion<IpNetwork2Converter>();
        }
    }
}
