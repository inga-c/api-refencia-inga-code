using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace IngaCode.TimeTracker.Web.Api.Extensions
{
    internal static class LocalizationExtension
    {
        public static void AddRequestLocalization(this IServiceCollection services)
        {
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var cultures = new List<CultureInfo>
                    {
                        new("pt-BR"),
                        new("en-US")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("pt-BR");
                    opts.SupportedCultures = cultures;
                    opts.SupportedUICultures = cultures;
                    opts.ApplyCurrentCultureToResponseHeaders = true;
                });
        }

        public static void UseLocalization(this IApplicationBuilder app)
        {
            var localizationOptions = app.ApplicationServices
                .GetRequiredService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);
        }
    }
}