using MetricsAgent.DTOs;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/clr")]
    public class CLRMetricsController : BaseAgentController<CLRMetricsDTO, MySqlConnection>
    {
        public CLRMetricsController(ILogger<CLRMetricsController> logger, MySqlConnection connector) : base(logger, connector)
        {
            _logger.LogDebug(1, "CLR Agent Metrics Controller");
        }

        [HttpGet("agent/{id}/errors-count/from/{fromTime}/to/{toTime}")]
        public override CLRMetricsDTO GetMetrics([FromRoute] string fromTime, [FromRoute] string toTime, [FromRoute] int id)
        {
            _logger.LogInformation($"Agent getting CLR metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                CLRMetricsRepository<MySqlConnection> repo = new CLRMetricsRepository<MySqlConnection>(_connector);

                CLRMetricsDTO dto = repo.GetByTimePeriod(from, to, id);

                _logger.LogInformation($"Agent {id} succesfully got CLR metrics");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }

        [HttpPost("post")]
        public override void PostMetrics([FromBody]CLRMetricsDTO dto)
        {
            _logger.LogInformation($"Agent {dto.id} posting CLR metrics.");

            try
            {
                CLRMetricsRepository<MySqlConnection> repo = new CLRMetricsRepository<MySqlConnection>(_connector);

                repo.Create(dto);

                _logger.LogInformation($"Agent {dto.id} succesfully posted CLR metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
