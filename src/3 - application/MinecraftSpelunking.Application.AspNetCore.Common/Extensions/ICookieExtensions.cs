using Microsoft.AspNetCore.Http;
using MinecraftSpelunking.Application.AspNetCore.Common.Dtos;
using MinecraftSpelunking.Application.AspNetCore.Common.Enums;
using MinecraftSpelunking.Application.AspNetCore.Common.Services;
using System.Diagnostics.CodeAnalysis;

namespace MinecraftSpelunking.Application.AspNetCore.Common.Extensions
{
    public static class ICookieExtensions
    {
        private static readonly CookieBuilder StatusMessageCookieBuilder = new()
        {
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true,
            MaxAge = TimeSpan.FromSeconds(5),
        };

        public static bool TryReadStatusMessage(this ICookieApplicationService cookies, [MaybeNullWhen(false)] out StatusMessageDto statusMessage)
        {
            string? typeString = cookies.Read(Constants.Cookies.StatusMessageCookieTypeName);
            string? value = cookies.Read(Constants.Cookies.StatusMessageCookieValueName);

            if (Enum.TryParse<StatusMessageTypeEnum>(typeString, out var type) == false || value is null)
            {
                statusMessage = default!;
                return false;
            }

            statusMessage = new StatusMessageDto()
            {
                Type = type,
                Value = value
            };

            return true;


        }

        public static void SetStatusMessage(this ICookieApplicationService cookies, StatusMessageDto statusMessage)
        {
            cookies.Set(Constants.Cookies.StatusMessageCookieTypeName, statusMessage.Type.ToString(), StatusMessageCookieBuilder);
            cookies.Set(Constants.Cookies.StatusMessageCookieValueName, statusMessage.Value, StatusMessageCookieBuilder);
        }
    }
}
