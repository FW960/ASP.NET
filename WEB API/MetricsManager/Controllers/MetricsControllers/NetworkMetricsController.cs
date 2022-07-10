using MetricsAgent.DTOs;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/manage/network")]
    [ApiController]
    public class NetworkMetricsController: IBaseMetricManagerController<NetworkMetricsDTO, MySqlConnection> 
    {
        protected readonly AgentsInfoValuesHolder<MySqlConnection> _holder;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, AgentsInfoValuesHolder<MySqlConnection> holder, MySqlConnection connector) : base(logger, connector)
        {
            _holder = holder;
            _logger.LogDebug(1, "Network Manager Metrics Controller.");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public override NetworkMetricsDTO GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting Network metrics from agent {agentId}. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                NetworkMetricsRepository<MySqlConnection> repo = new NetworkMetricsRepository<MySqlConnection>(_connector);

                NetworkMetricsDTO dto = repo.GetByTimePeriod(from, to, agentId);

                _logger.LogInformation($"Manager succesfully got Network metrics from agent {agentId}.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override NetworkMetricsDTO[] GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting Network metrics from all agents. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                NetworkMetricsRepository<MySqlConnection> repo = new NetworkMetricsRepository<MySqlConnection>(_connector);

                NetworkMetricsDTO[] dto = repo.GetByTimePeriod(from, to);

                _logger.LogInformation($"Manager succesfully got Network metrics from all agents.");

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

