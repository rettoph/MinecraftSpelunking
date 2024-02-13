using Microsoft.AspNetCore.Http;
using MinecraftSpelunking.Application.AspNetCore.Common.Services;

namespace MinecraftSpelunking.Application.AspNetCore.Services
{
    internal sealed class CookieApplicationService : ICookieApplicationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieApplicationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Delete(string cookie)
        {
            if (_httpContextAccessor.HttpContext?.Request.Cookies[cookie] is null)
            {
                return;
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
        }

        public string? Get(string cookie)
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies[cookie];
        }

        public string? Read(string cookie)
        {
            string? value = this.Get(cookie);

            if (value is not null)
            {
                this.Delete(cookie);
            }

            return value;
        }

        public void Set(string cookie, string value, CookieOptions options)
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return;
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookie, value, options);
        }

        public void Set(string cookie, string value, CookieBuilder builder)
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return;
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookie, value, builder.Build(_httpContextAccessor.HttpContext));
        }
    }
}
