using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {
        public static bool GetEndpointMetadataCollection(this HttpContext context, [MaybeNullWhen(false)] out EndpointMetadataCollection endpointMetadataCollection)
        {
            return (endpointMetadataCollection = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata) is not null;
        }

        public static T GetEndpointMetadataOrDefault<T>(this HttpContext context, T defaultValue)
            where T : class
        {
            if (context.GetEndpointMetadataCollection(out EndpointMetadataCollection? endpointMetadataCollection))
            {
                return endpointMetadataCollection.GetMetaDataOrDefault(defaultValue);
            }

            return defaultValue;
        }

        public static Uri ToRequestUri(this HttpContext context)
        {
            return new Uri(context.Request.GetEncodedUrl());
        }
    }
}
