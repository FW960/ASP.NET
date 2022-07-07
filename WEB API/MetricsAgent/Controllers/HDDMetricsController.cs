using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : IBaseAgentController
    {
        [HttpGet("api/metrics/hdd/left/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetrics([FromQuery] TimeSpan fromTime, TimeSpan toTime)
        {
            return Ok();
        }
    }
}
