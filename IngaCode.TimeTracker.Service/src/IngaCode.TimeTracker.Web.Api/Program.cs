using Asp.Versioning;
using IngaCode.TimeTracker.Web.Api.Converters;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using Serilog;
using System.Globalization;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging
    .AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

try
{
    logger.LogInformation("Inicializando a configuração da aplicação");

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.AllowInputFormatterExceptionMessages = true;
    });

    builder.Services.Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.SerializerOptions.Converters.Add(new DateTimeConverter());
        options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.SerializerOptions.PropertyNameCaseInsensitive = true;
    });

    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    }).AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    var allowedOrigins = builder.Configuration.GetSection("CorsAllowedOrigins").Get<string[]>();

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

    builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
    builder.Services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());

    builder.Services.AddLocalization();
    builder.Services.Configure<RequestLocalizationOptions>(
        opts =>
        {
            var cultures = new List<CultureInfo>
            {
            new CultureInfo("pt-BR"),
            new CultureInfo("en-US")
            };

            opts.DefaultRequestCulture = new RequestCulture("pt-BR");
            opts.SupportedCultures = cultures;
            opts.SupportedUICultures = cultures;
            opts.ApplyCurrentCultureToResponseHeaders = true;
        });


    logger.LogInformation("Inicializando a construção da aplicação.");
    var app = builder.Build();

    var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
    app.UseRequestLocalization(localizationOptions.Value);

    // Configure the HTTP request pipeline.

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    logger.LogInformation("Aplicação construída, iniciando a execução da aplicação.");
    app.Run();
}
catch (Exception exception)
{
    logger.LogCritical(exception, "Aplicação não pode ser iniciada, verifique a falha.");
}