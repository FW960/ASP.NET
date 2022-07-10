using MetricsAgent.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

public abstract class BaseAgentController<T, T2> : Controller where T : BaseDto where T2 : DbConnection
{
    protected T2 _connector;

    protected ILogger<BaseAgentController<T, T2>> _logger;

    public BaseAgentController(ILogger<BaseAgentController<T, T2>> logger, T2 connector)
    {
        _logger = logger;
        _connector = connector;
    }

    [HttpGet()]
    public abstract T GetMetrics([FromRoute] string fromTime, [FromRoute]string toTime, [FromRoute]int id);

    [HttpPost()]
    public abstract void PostMetrics(T dto);
}
