﻿using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : BaseMetricManagerController
    {
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

