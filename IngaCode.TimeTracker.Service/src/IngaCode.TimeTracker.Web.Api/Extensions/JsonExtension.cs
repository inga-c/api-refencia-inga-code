using System.Text.Json.Serialization;
using System.Text.Json;
using IngaCode.TimeTracker.Web.Api.Converters;
using Microsoft.AspNetCore.Http.Json;

namespace IngaCode.TimeTracker.Web.Api.Extensions
{
    internal static class JsonExtension
    {
        public static IMvcBuilder AddJsonJsonOptionsConfiguration(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options =>
            {
                options.AllowInputFormatterExceptionMessages = true;
            });

            return builder;
        }

        public static void AddJsonConfiguration(this IServiceCollection services)
        {
            services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.SerializerOptions.Converters.Add(new DateTimeConverter());
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.SerializerOptions.PropertyNameCaseInsensitive = true;
            });
        }
    }
}
