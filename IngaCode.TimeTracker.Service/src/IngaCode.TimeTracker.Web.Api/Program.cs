using IngaCode.TimeTracker.Web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddLoggerProvider();
var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

try
{
    logger.LogInformation("Inicializando a configura��o da aplica��o");

    builder.AddCorsConfiguration();
    builder.Services.AddDependencyInjection();

    builder.Services.AddControllers().AddJsonJsonOptionsConfiguration();
    builder.Services.AddJsonConfiguration();
    builder.Services.AddApiVersioningConfig();
    builder.Services.AddConfigurationCompression();
    builder.Services.AddRequestLocalization();
    

    logger.LogInformation("Inicializando a constru��o da aplica��o.");
    var app = builder.Build();

    app.UseLocalization();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    logger.LogInformation("Aplica��o constru�da, iniciando a execu��o da aplica��o.");
    app.Run();
}
catch (Exception exception)
{
    logger.LogCritical(exception, "Aplica��o n�o pode ser iniciada, verifique a falha.");
}