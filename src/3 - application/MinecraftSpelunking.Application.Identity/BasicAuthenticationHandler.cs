using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MinecraftSpelunking.Application.Identity.Common;
using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Application.Identity.Common.Enums;
using MinecraftSpelunking.Application.Identity.Common.Services;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace MinecraftSpelunking.Application.Identity
{
    internal sealed class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserApplicationService _users;

        public BasicAuthenticationHandler(IUserApplicationService users, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _users = users;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (this.Request.Headers.ContainsKey(Constants.Headers.Authorization) == false)
            {
                return AuthenticateResult.Fail($"Missing {Constants.Headers.Authorization} header.");
            }

            string? authorization = this.Request.Headers[Constants.Headers.Authorization];
            if (authorization is null)
            {
                return AuthenticateResult.Fail($"Missing {Constants.Headers.Authorization} value.");
            }
            if (authorization.StartsWith(Constants.AuthenticationSchemas.Basic, StringComparison.OrdinalIgnoreCase) == false)
            {
                return AuthenticateResult.Fail($"{Constants.Headers.Authorization} header does not start with 'Basic'.");
            }

            byte[] authorizationBase64 = Convert.FromBase64String(authorization.Replace("Basic", "", StringComparison.OrdinalIgnoreCase));
            string authorizationBase64Decode = Encoding.UTF8.GetString(authorizationBase64);
            string[] authorizationSplit = authorizationBase64Decode.Split(new[] { ':' }, 2);
            if (authorizationSplit.Length != 2)
            {
                return AuthenticateResult.Fail($"Invalid {Constants.Headers.Authorization} header format");
            }

            string email = authorizationSplit[0];
            string password = authorizationSplit[1];

            SignInResultDto signInResult = await _users.TrySignInWithEmailAndPasswordAsync(email, password, false);
            return signInResult.Type switch
            {
                SignInResultTypeEnum.Failure => AuthenticateResult.Fail($"Invalid {Constants.Headers.Authorization} header format"),
                SignInResultTypeEnum.Success => this.BuildSuccessResponse(signInResult.User!),
                _ => throw new NotImplementedException()
            };
        }

        private AuthenticateResult BuildSuccessResponse(UserDto user)
        {
            BasicAuthenticationClient client = new BasicAuthenticationClient()
            {
                AuthenticationType = Constants.AuthenticationSchemas.Basic,
                IsAuthenticated = true,
                Name = user.UserName
            };

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(client, new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            }));

            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
        }
    }
}
