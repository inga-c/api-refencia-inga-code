namespace IngaCode.TimeTracker.Web.Api.Extensions
{
    internal static class CorsExtension
    {
        public static void AddCorsConfiguration(this WebApplicationBuilder builder)
        {
            var allowedOrigins = builder.Configuration.GetValue<string[]>("CorsAllowedOrigins")!;

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PolicyCors", builder =>
                {
                    builder.WithOrigins(allowedOrigins)
                    .SetIsOriginAllowed(origin => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithExposedHeaders(["Location", "Content-Disposition"]);
                });
            });
        }
    }
}
