using MetricsAgent.DTOs;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/manage/clr")]
    [ApiController]
    public class CLRMetricsController : IBaseMetricManagerController<CLRMetricsDTO, MySqlConnection> 
    {
        protected readonly AgentsInfoValuesHolder<MySqlConnection> _holder;

        public CLRMetricsController(ILogger<CLRMetricsController> logger, AgentsInfoValuesHolder<MySqlConnection> holder, MySqlConnection connector) : base(logger, connector)
        {
            _holder = holder;
            _logger.LogDebug(1, "CLR Manager Metrics Controller.");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public override CLRMetricsDTO GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting CLR metrics from agent {agentId}. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                CLRMetricsRepository<MySqlConnection> repo = new CLRMetricsRepository<MySqlConnection>(_connector);

                CLRMetricsDTO dto = repo.GetByTimePeriod(from, to, agentId);

                _logger.LogInformation($"Manager succesfully got CLR metrics from agent {agentId}.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override CLRMetricsDTO[] GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting CLR metrics from all agents. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                CLRMetricsRepository<MySqlConnection> repo = new CLRMetricsRepository<MySqlConnection>(_connector);

                CLRMetricsDTO[] dto = repo.GetByTimePeriod(from, to);

                _logger.LogInformation($"Manager succesfully got CLR metrics from all agents.");

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

