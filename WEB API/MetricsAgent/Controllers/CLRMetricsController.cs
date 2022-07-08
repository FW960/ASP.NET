using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace MetricsAgent.Controllers
{
    [Route("metrics/clr")]
    public class CLRMetricsController : IBaseAgentController
    {
        private ILogger<CLRMetricsController> _logger;

        private MySqlConnection _connector;
        public CLRMetricsController(ILogger<CLRMetricsController> logger, MySqlConnection connector)
        {
            _logger = logger;
            _logger.LogDebug(1, "CLR Agent Metrics Controller");
            _connector = connector;
        }
        
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetrics([FromQuery] DateTime fromTime, DateTime toTime)
        {
            _logger.LogInformation($"Agent getting CLR metrics from {fromTime} to {toTime}");

            return Ok();
        }

    }
}
