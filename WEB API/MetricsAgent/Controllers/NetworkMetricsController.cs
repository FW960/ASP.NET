using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("metrics/network")]
    [ApiController]
    public class NetworkMetricsController : IBaseAgentController
    {
        private ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "Network Agent Metrics Controller.");
        }

        [HttpGet("api/metrics/network/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetrics([FromQuery] DateTime fromTime, DateTime toTime)
        {
            _logger.LogInformation($"Agent getting Network metrics from {fromTime} to {toTime}");

            return Ok();
        }
    }
}
