using MinecraftSpelunking.Application.Identity.Common.Enums;

namespace MinecraftSpelunking.Application.Identity.Common.Dtos
{
    public sealed class SignInResultDto
    {
        public static readonly SignInResultDto Failure = new SignInResultDto()
        {
            Type = SignInResultTypeEnum.Failure,
            User = null
        };

        public required SignInResultTypeEnum Type { get; init; }
        public required UserDto? User { get; init; }
    }
}
