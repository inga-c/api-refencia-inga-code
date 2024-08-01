using IngaCode.TimeTracker.Web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddLoggerProvider();
var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

try
{
    logger.LogInformation("Inicializando a configuração da aplicação");

    builder.AddCorsConfiguration();
    builder.Services.AddDependencyInjection();

    builder.Services.AddControllers().AddJsonJsonOptionsConfiguration();
    builder.Services.AddJsonConfiguration();
    builder.Services.AddApiVersioningConfig();
    builder.Services.AddConfigurationCompression();
    builder.Services.AddRequestLocalization();
    

    logger.LogInformation("Inicializando a construção da aplicação.");
    var app = builder.Build();

    app.UseLocalization();
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