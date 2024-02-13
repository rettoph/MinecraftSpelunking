using AutoMapper;
using MinecraftSpelunking.Application.Minecraft.Common.Dtos;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Application.Minecraft.Common.AutoMapper
{
    public sealed class ApplicationMinecraftMapperProfile : Profile
    {
        public ApplicationMinecraftMapperProfile()
        {
            this.CreateMap<AddressBlockAssignment, AddressBlockAssignmentDto>();
            this.CreateMap<AddressBlockAssignmentDto, AddressBlockAssignment>();

            this.CreateMap<AddressBlock, AddressBlockDto>();
            this.CreateMap<AddressBlockDto, AddressBlock>();

            this.CreateMap<JavaServer, JavaServerDto>();
            this.CreateMap<JavaServerDto, JavaServer>();

            this.CreateMap<Mod, ModDto>();
            this.CreateMap<ModDto, Mod>();

            this.CreateMap<ModPackData, ModPackDataDto>();
            this.CreateMap<ModPackDataDto, ModPackData>();

            this.CreateMap<ModType, ModTypeDto>();
            this.CreateMap<ModTypeDto, ModType>();

            this.CreateMap<ModVersion, ModVersionDto>();
            this.CreateMap<ModVersionDto, ModVersion>();

            this.CreateMap<ReservedAddressBlock, ReservedAddressBlockDto>();
            this.CreateMap<ReservedAddressBlockDto, ReservedAddressBlock>();

            this.CreateMap<Server, ServerDto>();
            this.CreateMap<ServerDto, Server>();

            this.CreateMap<ServerIcon, ServerIconDto>();
            this.CreateMap<ServerIconDto, ServerIcon>();
        }
    }
}
