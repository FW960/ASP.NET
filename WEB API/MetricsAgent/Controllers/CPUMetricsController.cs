using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{

    [Route("metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : IBaseAgentController
    {
        private ILogger<CPUMetricsController> _logger;
        public CPUMetricsController(ILogger<CPUMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "CPU Agent Metrics Controller.");
        }

        [HttpGet("api/metrics/cpu/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetrics([FromQuery] DateTime fromTime, DateTime toTime)
        {
            _logger.LogInformation($"Agent getting CPU metrics from {fromTime} to {toTime}");

            return Ok();
        }
    }
}
