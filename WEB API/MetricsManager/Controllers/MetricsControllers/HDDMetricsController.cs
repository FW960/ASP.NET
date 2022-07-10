using MetricsAgent.DTOs;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/manage/hdd")]
    [ApiController]
    public class HDDMetricsController : IBaseMetricManagerController<HDDMetricsDTO, MySqlConnection>
    {
        protected readonly AgentsInfoValuesHolder<MySqlConnection> _holder;

        public HDDMetricsController(ILogger<HDDMetricsController> logger, AgentsInfoValuesHolder<MySqlConnection> holder, MySqlConnection connector) : base(logger, connector)
        {
            _holder = holder;
            _logger.LogDebug(1, "HDD Manager Metrics Controller.");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public override HDDMetricsDTO GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting HDD metrics from agent {agentId}. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                HDDMetricsRepository<MySqlConnection> repo = new HDDMetricsRepository<MySqlConnection>(_connector);

                HDDMetricsDTO dto = repo.GetByTimePeriod(from, to, agentId);

                _logger.LogInformation($"Manager succesfully got HDD metrics from agent {agentId}.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override HDDMetricsDTO[] GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting CPU metrics from all agents. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                HDDMetricsRepository<MySqlConnection> repo = new HDDMetricsRepository<MySqlConnection>(_connector);

                HDDMetricsDTO[] dto = repo.GetByTimePeriod(from, to);

                _logger.LogInformation($"Manager succesfully got CPU metrics from all agents.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }

}
