namespace MinecraftSpelunking.Application.Identity.Common.Dtos
{
    public class RegisterUserResultDto
    {
        public IdentityResultDto IdentityResult { get; set; } = new IdentityResultDto();
        public UserDto? User { get; set; }
    }
}
