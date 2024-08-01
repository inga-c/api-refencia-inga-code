using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace IngaCode.TimeTracker.Web.Api.Extensions
{
    internal static class CompressionExtension
    {
        public static void AddConfigurationCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());
        }
    }
}