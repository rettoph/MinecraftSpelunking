using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Domain.Identity.Common.Entities;

namespace MinecraftSpelunking.Application.Identity.Common.AutoMapper
{
    public sealed class ApplicationAccountMapperProfile : Profile
    {
        public ApplicationAccountMapperProfile()
        {
            this.CreateMap<User, UserDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName));

            this.CreateMap<IdentityResult, IdentityResultDto>()
                .ForMember(d => d.Succeeded, o => o.MapFrom(s => s.Succeeded))
                .ForMember(d => d.Errors, o => o.MapFrom(s => s.Errors.ToList()));
        }
    }
}
