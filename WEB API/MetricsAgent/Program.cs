using NLog.Web;
using NLog;
using MySqlConnector;
using System.Text.Json;
using MetricsManager;
using MetricsEntetiesAndFunctions.Entities;
using Microsoft.EntityFrameworkCore;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;
using MetricsEntetiesAndFunctions.Functions.Quartz;
using System.Text;
using DTOs;
using System.Collections.Concurrent;

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

    builder.Services.AddDbContext<MyDbContext>(options => options.UseMySql(connectionStrings.Default, ServerVersion.AutoDetect(connectionStrings.Default)));

    builder.Services.AddSingleton<List<CPUMetricsDTO>>();

    builder.Services.AddSingleton<List<NetworkMetricsDTO>>();

    builder.Services.AddSingleton<List<HDDMetricsDTO>>();

    builder.Services.AddSingleton<List<CLRMetricsDTO>>();

    builder.Services.AddSingleton<List<RAMMetricsDTO>>();

    AgentInfo agent = RegisterAgent();

    builder.Services.AddSingleton(agent);

    builder.Services.AddSingleton<IJobFactory, JobFactory>();
    builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

    builder.Services.AddSingleton<CollectMetricsJob>();
    builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(CollectMetricsJob),
    cronExpression: "0/5 * * * * ?"));

    builder.Services.AddHostedService<QuartzHostedService>();

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

AgentInfo RegisterAgent()
{

    HttpClient httpClient = new HttpClient();

    var pcNameJson = JsonSerializer.Serialize(Environment.MachineName);

    HttpContent content = new StringContent(pcNameJson, Encoding.UTF8, "application/json");

    HttpResponseMessage agentResponse = httpClient.PostAsync("http://localhost:7139/agents/manage/register", content).Result;

    string agentJson = agentResponse.Content.ReadAsStringAsync().Result;

    AgentInfo agent = JsonSerializer.Deserialize<AgentInfo>(agentJson, new JsonSerializerOptions(JsonSerializerDefaults.Web));

    return agent;
}