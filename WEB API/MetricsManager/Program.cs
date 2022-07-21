using MetricsManager;
using NLog.Web;
using NLog;
using System.Text.Json;
using MySqlConnector;
using MetricsManager.Controllers.MetricsControllers;
using MetricsEntetiesAndFunctions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

Logger logger = NLogBuilder.ConfigureNLog("nLog.config").GetCurrentClassLogger();
logger.Debug("init main");

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

    builder.Services.AddSingleton<MySqlConnection>(new MySqlConnection(connectionStrings?.Default));

    builder.Services.AddDbContext<MyDbContext>(options => options.UseMySql(connectionStrings.Default, ServerVersion.AutoDetect(connectionStrings.Default)));

    builder.Services.AddHttpClient();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "WebPage")),
        RequestPath = "/WebPage",
        EnableDefaultFiles = true
    });

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    logger.Factory?.Shutdown();
}







