using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Domain.Identity.Common.Enums;

namespace MinecraftSpelunking.Application.Identity.Common.Services
{
    public interface IUserApplicationService
    {
        Task<RegisterUserResultDto> RegisterUserAsync(string username, string email, string password, params UserRoleTypeEnum[] roles);

        Task<SignInResultDto> TrySignInWithEmailAndPasswordAsync(string email, string password, bool isPersistent);

        Task SignOutAsync();

        Task<UserDto?> GetCurrentUserAsync();
    }
}
