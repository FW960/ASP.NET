using MetricsAgent.DTOs;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using NLog;
using System.Data.Common;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/manage/ram")]
    [ApiController]
    public class RAMMetricsController : IBaseMetricManagerController<RAMMetricsDTO, MySqlConnection>
    {
        protected readonly AgentsInfoValuesHolder<MySqlConnection> _holder;
        public RAMMetricsController(ILogger<RAMMetricsController> logger, AgentsInfoValuesHolder<MySqlConnection> holder, MySqlConnection connector) : base(logger, connector)
        {
            _holder = holder;
            _logger.LogDebug(1, "RAM Manager Metrics Controller.");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public override RAMMetricsDTO GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting RAM metrics from agent {agentId}. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                RAMMetricsRepository<MySqlConnection> repo = new RAMMetricsRepository<MySqlConnection>(_connector);

                RAMMetricsDTO dto = repo.GetByTimePeriod(from, to, agentId);

                _logger.LogInformation($"Manager succesfully got RAM metrics from agent {agentId}.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override RAMMetricsDTO[] GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting RAM metrics from all agents. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                RAMMetricsRepository<MySqlConnection> repo = new RAMMetricsRepository<MySqlConnection>(_connector);

                RAMMetricsDTO[] dto = repo.GetByTimePeriod(from, to);

                _logger.LogInformation($"Manager succesfully got RAM metrics from all agents.");

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

