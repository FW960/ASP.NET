using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{

    [Route("metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : BaseAgentController
    {
        [HttpGet("api/metrics/cpu/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetrics([FromQuery] TimeSpan fromTime, TimeSpan toTime)
        {
            return Ok();
        }
    }
}
