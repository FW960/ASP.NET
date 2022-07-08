using Microsoft.AspNetCore.Mvc;
using NLog;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/ram")]
    [ApiController]
    public class RAMMetricsController : IBaseMetricManagerController
    {
        private readonly ILogger<RAMMetricsController> _logger;

        private readonly AgentsInfoValuesHolder _holder;

        public RAMMetricsController(ILogger<RAMMetricsController> logger, AgentsInfoValuesHolder holder)
        {
            _logger = logger;
            _logger.LogDebug(1, "RAM Manager Metrics Controller.");
            _holder = holder;

        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Manager getting RAM metrics from agent {agentId}. Time: from {fromTime.Seconds} to {toTime.Seconds}");

            return Ok();
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Manager getting RAM metrics from all agents. Time: from {fromTime.Seconds} to {toTime.Seconds}");

            return Ok();
        }
    }

}

