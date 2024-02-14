﻿using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Json
{
    public sealed class IPNetwork2Converter : JsonConverter<IPNetwork2>
    {
        public override IPNetwork2? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IPNetwork2 value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
