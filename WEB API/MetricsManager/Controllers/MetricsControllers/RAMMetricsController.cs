using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/ram")]
    [ApiController]
    public class RAMMetricsController : IBaseMetricManagerController
    {
        private readonly ILogger<RAMMetricsController> _logger;

        private readonly AgentsInfoValuesHolder _holder;

        public RAMMetricsController(ILogger logger)
        {
        }

        public RAMMetricsController(ILogger<RAMMetricsController> logger, AgentsInfoValuesHolder holder)
        {
            _logger = logger;
            _holder = holder;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }

}

