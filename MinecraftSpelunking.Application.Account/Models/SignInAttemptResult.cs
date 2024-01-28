using MinecraftSpelunking.Common.Account;
using MinecraftSpelunking.Common.Account.Entities;

namespace MinecraftSpelunking.Application.Account.Models
{
    public class SignInAttemptResult
    {
        public required SignInAttemptResultEnum Result { get; init; }
        public required User? User { get; init; }

        public static SignInAttemptResult Success(User user)
        {
            return new SignInAttemptResult()
            {
                Result = SignInAttemptResultEnum.Success,
                User = user
            };
        }

        public static SignInAttemptResult Failure()
        {
            return new SignInAttemptResult()
            {
                Result = SignInAttemptResultEnum.Failure,
                User = null
            };
        }
    }
}
