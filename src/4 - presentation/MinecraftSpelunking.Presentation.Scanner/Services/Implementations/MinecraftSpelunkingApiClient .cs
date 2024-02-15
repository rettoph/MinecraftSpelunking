using Microsoft.AspNetCore.Http.Extensions;
using MinecraftSpelunking.Domain.Minecraft.Common.Json;
using MinecraftSpelunking.Presentation.Common;
using MinecraftSpelunking.Presentation.Common.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace MinecraftSpelunking.Presentation.Scanner.Implementations
{
    internal sealed class MinecraftSpelunkingApiClient : IMinecraftSpelunkingApiClient
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new IPNetwork2Converter()
            }
        };

        public MinecraftSpelunkingApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<AddressBlockAssignmentModel?> TryCompleteAsync(AddressBlockAssignmentResultsModel previous)
        {
            UriBuilder uri = new UriBuilder(_http.BaseAddress ?? throw new Exception());
            uri.Path = Constants.Routes.Api.v1.AddressBlockAssignment.Complete;

            HttpResponseMessage response = await _http.PostAsJsonAsync(uri.Uri, previous);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            Response<AddressBlockAssignmentModel>? content = await response.Content.ReadFromJsonAsync<Response<AddressBlockAssignmentModel>>(_jsonOptions);

            return content?.Model;
        }

        public async Task<AddressBlockAssignmentsModel?> TryGetAsync(int count)
        {
            UriBuilder uri = new UriBuilder(_http.BaseAddress ?? throw new Exception());
            uri.Path = Constants.Routes.Api.v1.AddressBlockAssignment.Get;
            uri.Query = new QueryBuilder()
            {
                { "count", count.ToString() }
            }.ToString();

            HttpResponseMessage response = await _http.GetAsync(uri.Uri);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            Response<AddressBlockAssignmentsModel>? content = await response.Content.ReadFromJsonAsync<Response<AddressBlockAssignmentsModel>>(_jsonOptions);

            return content?.Model;
        }
    }
}
