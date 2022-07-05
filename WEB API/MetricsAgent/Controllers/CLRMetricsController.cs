using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("metrics/clr")]
    public class CLRMetricsController : BaseAgentController
    {
        
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetrics([FromQuery] TimeSpan fromTime, TimeSpan toTime)
        {
            return Ok();
        }

    }
}
