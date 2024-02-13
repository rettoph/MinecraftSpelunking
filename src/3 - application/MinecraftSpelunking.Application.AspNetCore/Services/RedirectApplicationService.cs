using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using MinecraftSpelunking.Application.AspNetCore.Common.Dtos;
using MinecraftSpelunking.Application.AspNetCore.Common.Extensions;
using MinecraftSpelunking.Application.AspNetCore.Common.Services;

namespace MinecraftSpelunking.Application.AspNetCore.Services
{
    internal sealed class RedirectApplicationService : IRedirectApplicationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICookieApplicationService _cookies;
        private readonly NavigationManager _navigationManager;

        public RedirectApplicationService(NavigationManager navigationManager, ICookieApplicationService cookies, IHttpContextAccessor httpContextAccessor)
        {
            _cookies = cookies;
            _navigationManager = navigationManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public void RedirectTo(string uri, Dictionary<string, object?>? queryParameters = null, StatusMessageDto? statusMessage = null)
        {
            if (statusMessage is not null)
            {
                _cookies.SetStatusMessage(statusMessage);
            }

            if (this.TryRedirectTo_NavigationManager(uri, queryParameters) == false)
            {
                this.TryRedirectTo_HttpRequest(uri, queryParameters);
            }
        }

        private bool TryRedirectTo_NavigationManager(string uri, Dictionary<string, object?>? queryParameters)
        {
            try
            {
                uri ??= string.Empty;

                if (queryParameters is not null)
                {
                    string uriWithoutQuery = _navigationManager.ToAbsoluteUri(uri).GetLeftPart(UriPartial.Path);
                    uri = _navigationManager.GetUriWithQueryParameters(uriWithoutQuery, queryParameters);
                }

                // Prevent open redirects.
                if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
                {
                    uri = _navigationManager.ToBaseRelativePath(uri);
                }

                // During static rendering, NavigateTo throws a NavigationException which is handled by the framework as a redirect.
                // So as long as this is called from a statically rendered Identity component, the InvalidOperationException is never thrown.
                _navigationManager.NavigateTo(uri);
                throw new InvalidOperationException($"{nameof(RedirectApplicationService)} can only be used during static rendering.");
            }
            catch (InvalidOperationException ex)
            {
                return false;
            }
        }

        private bool TryRedirectTo_HttpRequest(string uri, Dictionary<string, object?>? queryParameters)
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return false;
            }

            uri ??= string.Empty;

            if (queryParameters is not null)
            {
                string uriWithoutQuery = new Uri(_httpContextAccessor.HttpContext.ToRequestUri(), uri).GetLeftPart(UriPartial.Path);
                uri = QueryHelpers.AddQueryString(uriWithoutQuery, queryParameters.Select(x => new KeyValuePair<string, string?>(x.Key, x.Value?.ToString())));
            }

            _httpContextAccessor.HttpContext.Response.Redirect(uri);
            return true;
        }
    }
}
