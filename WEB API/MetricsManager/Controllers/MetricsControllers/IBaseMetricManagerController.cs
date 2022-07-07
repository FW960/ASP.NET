using Microsoft.AspNetCore.Mvc;

namespace MetricsManager
{
    public abstract class IBaseMetricManagerController : Controller
    {
        public abstract IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime);

        public abstract IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime);
    }
}
