using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : IBaseMetricManagerController
    {
        private readonly ILogger<CPUMetricsController> _logger;

        private readonly AgentsInfoValuesHolder _holder;

        public CPUMetricsController(ILogger<CPUMetricsController> logger, AgentsInfoValuesHolder holder)
        {
            _logger = logger;
            _logger.LogDebug(1, "CPU Manager Metrics Controller.");
            _holder = holder;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Manager getting CPU metrics from agent {agentId}. Time: from {fromTime.Seconds} to {toTime.Seconds}");

            return Ok();
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Manager getting CPU metrics from all agents. Time: from {fromTime.Seconds} to {toTime.Seconds}");

            return Ok();
        }
    }

}

