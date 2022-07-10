using MetricsAgent.DTOs;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Controllers
{

    [Route("metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : BaseAgentController<CPUMetricsDTO, MySqlConnection>
    {
        public CPUMetricsController(ILogger<CPUMetricsController> logger, MySqlConnection connector) : base(logger, connector)
        {
            _logger.LogDebug(1, "CPU Agent Metrics Controller.");
        }

        [HttpGet("agent/{id}/from/{fromTime}/to/{toTime}")]
        public override CPUMetricsDTO GetMetrics([FromRoute] string fromTime, [FromRoute] string toTime, [FromRoute] int id)
        {
            _logger.LogInformation($"Agent getting CPU metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                CPUMetricsRepository<MySqlConnection> repo = new CPUMetricsRepository<MySqlConnection>(_connector);

                CPUMetricsDTO dto = repo.GetByTimePeriod(from, to, id);

                _logger.LogInformation($"Agent {id} succesfully got CPU metrics");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpPost("post")]
        public override void PostMetrics(CPUMetricsDTO dto)
        {
            _logger.LogInformation($"Agent {dto.id} posting CPU metrics.");

            try
            {
                CPUMetricsRepository<MySqlConnection> repo = new CPUMetricsRepository<MySqlConnection>(_connector);

                repo.Create(dto);

                _logger.LogInformation($"Agent {dto.id} succesfully posted CPU metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
