using Microsoft.AspNetCore.Mvc;

public abstract class IBaseAgentController : Controller
{
    [HttpGet()]
    public abstract IActionResult GetMetrics([FromQuery] TimeSpan fromTime, TimeSpan toTime);
}
