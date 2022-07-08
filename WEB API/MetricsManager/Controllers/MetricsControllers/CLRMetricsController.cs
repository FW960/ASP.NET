using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/clr")]
    [ApiController]
    public class CLRMetricsController : IBaseMetricManagerController
    {
        private readonly ILogger<CLRMetricsController> _logger;

        private readonly AgentsInfoValuesHolder _holder;

        public CLRMetricsController(ILogger<CLRMetricsController> logger, AgentsInfoValuesHolder holder)
        {
            _logger = logger;
            _logger.LogDebug(1, "CLR Manager Metrics Controller.");
            _holder = holder;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Manager getting CLR metrics from agent {agentId}. Time: from {fromTime.Seconds} to {toTime.Seconds}");

            return Ok();
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Manager getting CLR metrics from all agents. Time: from {fromTime.Seconds} to {toTime.Seconds}");

            return Ok();
        }
    }

}

