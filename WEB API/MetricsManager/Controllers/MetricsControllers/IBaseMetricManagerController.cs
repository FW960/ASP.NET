using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace MetricsManager
{
    public abstract class IBaseMetricManagerController<T, T2> : Controller where T : BaseDto where T2 : DbContext
    {
        protected ILogger<IBaseMetricManagerController<T, T2>> _logger; 

        protected T2 _dbContext;

        protected IBaseMetricManagerController(ILogger<IBaseMetricManagerController<T, T2>> logger, T2 connector)
        {
            _logger = logger;
            _dbContext = connector;
        }
        public abstract T GetMetricsFromAgent([FromRoute] int id, [FromRoute] string fromTime, [FromRoute] string toTime);

        public abstract List<T> GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime);

        public abstract List<T> GetAllMetricsFromAgent([FromRoute] int id);
    }
}
