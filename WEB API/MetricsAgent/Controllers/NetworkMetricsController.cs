using MetricsAgent.DTOs;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/network")]
    [ApiController]
    public class NetworkMetricsController : BaseAgentController<NetworkMetricsDTO, MySqlConnection> 
    {
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, MySqlConnection connector) : base(logger, connector)
        {
            _logger.LogDebug(1, "Network Agent Metrics Controller.");
        }

        [HttpGet("agent/{id}/from/{fromTime}/to/{toTime}")]
        public override NetworkMetricsDTO GetMetrics([FromRoute] string fromTime, [FromRoute]string toTime, [FromRoute]int id)
        {
            _logger.LogInformation($"Agent {id} getting Network metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                NetworkMetricsRepository<MySqlConnection> repo = new NetworkMetricsRepository<MySqlConnection>(_connector);

                NetworkMetricsDTO dto = repo.GetByTimePeriod(from, to, id);

                _logger.LogInformation($"Agent {id} succesfully got Network metrics");

                return dto;

            }catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpPost("post")]
        public override void PostMetrics(NetworkMetricsDTO dto)
        {
            _logger.LogInformation($"Agent {dto.id} posting Network metrics.");

            try
            {
                NetworkMetricsRepository<MySqlConnection> repo = new NetworkMetricsRepository<MySqlConnection>(_connector);

                repo.Create(dto);

                _logger.LogInformation($"Agent {dto.id} succesfully posted Network metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
