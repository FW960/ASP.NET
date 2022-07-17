using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Data.Common;

public abstract class BaseAgentController<T, T2> : Controller where T : BaseDto where T2 : DbContext
{
    protected T2 _dbContext;

    protected ILogger<BaseAgentController<T, T2>> _logger;

    protected List<T> _records;

    protected AgentInfo _agentInfo;

    public BaseAgentController(ILogger<BaseAgentController<T, T2>> logger, T2 dbContext, List<T> records, AgentInfo agentInfo)
    {
        _logger = logger;
        _dbContext = dbContext;
        _records = records;
        _agentInfo = agentInfo;
    }

    [HttpGet()]
    public abstract List<T> GetMetrics([FromRoute] string fromTime, [FromRoute] string toTime);

    [HttpPost()]
    public abstract void PostMetrics([FromBody] List<T> dto);
}
