using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

public abstract class BaseAgentController<T, T2> : Controller where T : BaseDto where T2 : DbContext
{
    protected T2 _dbContext;

    protected ILogger<BaseAgentController<T, T2>> _logger;

    public BaseAgentController(ILogger<BaseAgentController<T, T2>> logger, T2 dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet()]
    public abstract T GetMetrics([FromRoute] string fromTime, [FromRoute]string toTime, [FromRoute]int id);

    [HttpPost()]
    public abstract void PostMetrics(T dto);
}
