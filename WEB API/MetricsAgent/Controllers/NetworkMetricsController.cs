using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("metrics/network")]
    [ApiController]
    public class NetworkMetricsController : BaseAgentController
    {
        [HttpGet("api/metrics/network/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetrics([FromQuery] TimeSpan fromTime, TimeSpan toTime)
        {
            return Ok();
        }
    }
}
