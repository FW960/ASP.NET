using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("metrics/ram")]
    [ApiController]
    public class RAMMetricsController : IBaseAgentController
    {
        private ILogger<RAMMetricsController> _logger;

        public RAMMetricsController(ILogger<RAMMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "RAM Agent Metrics Logger");
        }

        [HttpGet("api/metrics/ram/available/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetrics([FromQuery] DateTime fromTime, DateTime toTime)
        {
            _logger.LogInformation($"Agent getting RAM metrics from {fromTime} to {toTime}");

            return Ok();
        }
    }
}
