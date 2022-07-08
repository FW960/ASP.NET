﻿using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/network")]
    [ApiController]
    public class NetworkMetricsController : IBaseMetricManagerController
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        private readonly AgentsInfoValuesHolder _holder;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, AgentsInfoValuesHolder holder)
        {
            _logger = logger;
            _logger.LogDebug(1, "Network Manager Metrics Controller.");
            _holder = holder;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Manager getting Network metrics from agent {agentId}. Time: from {fromTime.Seconds} to {toTime.Seconds}");

            return Ok();
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Manager getting Network metrics from all agents. Time: from {fromTime.Seconds} to {toTime.Seconds}");

            return Ok();
        }
    }

}

