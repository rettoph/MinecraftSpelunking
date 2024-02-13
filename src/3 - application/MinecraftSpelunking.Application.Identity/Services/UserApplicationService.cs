using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Application.Identity.Common.Services;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Identity.Common.Enums;
using MinecraftSpelunking.Domain.Identity.Common.Services;

namespace MinecraftSpelunking.Application.Identity.Services
{
    internal sealed class UserApplicationService : IUserApplicationService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _users;
        private readonly IMapper _mapper;

        public UserApplicationService(SignInManager<User> signInManager, UserManager<User> userManager, IUserService users, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _users = users;
            _mapper = mapper;
        }

        public async Task<RegisterUserResultDto> RegisterUserAsync(string username, string email, string password, params UserRoleTypeEnum[] roles)
        {
            RegisterUserResultDto result = new RegisterUserResultDto();
            User user = new User()
            {
                UserName = username,
                NormalizedUserName = username,
                Email = email,
                NormalizedEmail = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult createUserResult = await _userManager.CreateAsync(user);
            if (result.IdentityResult.Merge(createUserResult) && roles.Length > 0)
            {
                IdentityResult addToRolesResult = await _userManager.AddToRolesAsync(user, roles);
                if (result.IdentityResult.Merge(addToRolesResult) == false)
                {
                    IdentityResult deleteResult = await _userManager.DeleteAsync(user);
                    result.IdentityResult.Merge(deleteResult);
                }
            }

            if (result.IdentityResult.Succeeded)
            {
                result.User = _mapper.Map<UserDto>(user);
            }

            return result;
        }

        public async Task<SignInResultDto> TrySignInWithEmailAndPasswordAsync(string email, string password)
        {
            User? user = await _users.GetUserByEmailAsync(email);
            if (user is null)
            {
                return SignInResultDto.Failure;
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);
            return new SignInResultDto()
            {
                Type = signInResult.ToSignInResultTypeEnum(),
                User = _mapper.Map<UserDto?>(user)
            };
        }
    }
}
