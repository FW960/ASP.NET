using MetricsAgent.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace MetricsManager
{
    public abstract class IBaseMetricManagerController<T, T2> : Controller where T : BaseDto where T2 : DbConnection
    {
        protected ILogger<IBaseMetricManagerController<T, T2>> _logger; 

        protected T2 _connector;

        protected IBaseMetricManagerController(ILogger<IBaseMetricManagerController<T, T2>> logger, T2 connector)
        {
            _logger = logger;
            _connector = connector;
        }
        public abstract T GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] string fromTime, [FromRoute] string toTime);

        public abstract T[] GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime);
    }
}
