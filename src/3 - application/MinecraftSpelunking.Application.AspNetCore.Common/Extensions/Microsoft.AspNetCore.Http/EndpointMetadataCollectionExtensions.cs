namespace Microsoft.AspNetCore.Http
{
    public static class EndpointMetadataCollectionExtensions
    {
        public static T GetMetaDataOrDefault<T>(this EndpointMetadataCollection endpointMetadataCollection, T defaultValue)
            where T : class
        {
            return endpointMetadataCollection.GetMetadata<T>() ?? defaultValue;
        }
    }
}
