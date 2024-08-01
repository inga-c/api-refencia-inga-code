using Serilog;

namespace IngaCode.TimeTracker.Web.Api.Extensions
{
    internal static class LoggerExtension
    {
        public static void AddLoggerProvider(this IHostApplicationBuilder builder) =>
            builder.Logging
                .ClearProviders()
                .AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());
    }
}
