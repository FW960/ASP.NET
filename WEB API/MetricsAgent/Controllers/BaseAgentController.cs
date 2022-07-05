using Microsoft.AspNetCore.Mvc;

public abstract class BaseAgentController : Controller
{
    [HttpGet()]
    public abstract IActionResult GetMetrics([FromQuery] TimeSpan fromTime, TimeSpan toTime);
}
