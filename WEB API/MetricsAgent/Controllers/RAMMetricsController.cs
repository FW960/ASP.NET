using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("metrics/ram")]
    [ApiController]
    public class RAMMetricsController : IBaseAgentController
    {
        [HttpGet("api/metrics/ram/available/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetrics([FromQuery] TimeSpan fromTime, TimeSpan toTime)
        {
            return Ok();
        }
    }
}
