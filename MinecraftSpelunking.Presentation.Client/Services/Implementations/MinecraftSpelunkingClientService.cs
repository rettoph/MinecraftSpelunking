﻿using Microsoft.Extensions.Options;
using MinecraftSpelunking.Presentation.Client.Configurations;
using MinecraftSpelunking.Presentation.Client.Models;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Json;
using System.Web;

namespace MinecraftSpelunking.Presentation.Client.Services.Implementations
{
    public sealed class MinecraftSpelunkingClientService : IMinecraftSpelunkingClientService
    {
        private const string AccessTokenQueryParameter = "access_token";

        private readonly HttpClient _client;
        private readonly string _baseAddress;
        private readonly string _accessToken;

        public MinecraftSpelunkingClientService(IOptions<MinecraftSpelunkingClientConfiguration> configuration)
        {
            _client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(60)
            };
            _baseAddress = configuration.Value.BaseAddress;
            _accessToken = configuration.Value.AccessToken;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<HttpResponse<TResponse>> PostAsync<TBody, TResponse>(string path, TBody body, NameValueCollection? query = null)
        {
            UriBuilder uriBuilder = new UriBuilder(_baseAddress);
            uriBuilder.Path = path;

            query ??= HttpUtility.ParseQueryString(uriBuilder.Query);
            query[AccessTokenQueryParameter] = _accessToken;

            uriBuilder.Query = query.ToString();

            HttpResponseMessage? response = null;

            int retry = 0;
            while (retry < 5)
            {
                try
                {
                    response = await _client.PostAsJsonAsync(uriBuilder.ToString(), body);
                    break;
                }
                catch
                {
                    retry++;
                }
            }


            if (response is null)
            {
                return new HttpResponse<TResponse>()
                {
                    StatusCode = HttpStatusCode.RequestTimeout,
                    Content = default
                };
            }

            TResponse? content = await response.Content.ReadFromJsonAsync<TResponse>();


            return new HttpResponse<TResponse>()
            {
                StatusCode = response.StatusCode,
                Content = content
            };
        }
    }
}
