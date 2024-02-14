using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Database.Converters
{
    public sealed class IPNetwork2Converter : ValueConverter<IPNetwork2, string>
    {
        public IPNetwork2Converter() : base(v => v.ToString(), v => IPNetwork2.Parse(v))
        {
        }

        public static void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<IPNetwork2>().HaveConversion<IPNetwork2Converter>();
        }
    }
}
