using MinecraftSpelunking.Application.Identity.Common.Enums;

namespace Microsoft.AspNetCore.Identity
{
    public static class SignInResultExtensions
    {
        public static SignInResultTypeEnum ToSignInResultTypeEnum(this SignInResult result)
        {
            return result switch
            {
                { Succeeded: true } => SignInResultTypeEnum.Success,
                _ => SignInResultTypeEnum.Failure
            };
        }
    }
}
