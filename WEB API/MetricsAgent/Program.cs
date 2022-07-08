using NLog.Web;
using NLog;
using MySqlConnector;
using System.Text.Json;
using MetricsManager;

Logger logger = NLogBuilder.ConfigureNLog("nLog.config").GetCurrentClassLogger();
logger.Debug("init.main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();

    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

    builder.Host.UseNLog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    string connectionText = File.ReadAllText("connectionSettings.json");

    ConnectionStrings? connectionStrings = JsonSerializer.Deserialize<ConnectionStrings>(connectionText);

    builder.Services.AddSingleton<ConnectionStrings>(connectionStrings);

    builder.Services.AddSingleton<MySqlConnection>(new MySqlConnection(connectionStrings?.Default));

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Program stopped because of logger");
    throw;
}
finally
{
    logger.Factory.Shutdown();
}






