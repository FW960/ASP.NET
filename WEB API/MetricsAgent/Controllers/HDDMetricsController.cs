using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : IBaseAgentController
    {
        private ILogger<HDDMetricsController> _logger;

        public HDDMetricsController(ILogger<HDDMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "HDD Agent Metrics Controller.");
        }

        [HttpGet("api/metrics/hdd/left/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetrics([FromQuery] DateTime fromTime, DateTime toTime)
        {
            _logger.LogInformation($"Agent getting HDD metrics from {fromTime} to {toTime}");

            return Ok();
        }
    }
}
