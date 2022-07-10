using MetricsAgent.DTOs;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : BaseAgentController<HDDMetricsDTO, MySqlConnection> 
    {
        public HDDMetricsController(ILogger<HDDMetricsController> logger, MySqlConnection connector) : base(logger, connector)
        {
            _logger.LogDebug(1, "HDD Agent Metrics Controller.");
        }

        [HttpGet("agent/{id}/left/from/{fromTime}/to/{toTime}")]
        public override HDDMetricsDTO GetMetrics([FromRoute] string fromTime, [FromRoute]string toTime, [FromRoute] int id)
        {
            _logger.LogInformation($"Agent getting HDD metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);
            
            DateTime to = DateTime.Parse(toTime);

            try
            {
                HDDMetricsRepository<MySqlConnection> repo = new HDDMetricsRepository<MySqlConnection>(_connector);

                HDDMetricsDTO dto = repo.GetByTimePeriod(from, to, id);

                _logger.LogInformation($"Agent {id} succesfully got HDD metrics");

                return dto;
            }catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpPost("post")]
        public override void PostMetrics(HDDMetricsDTO dto)
        {
            _logger.LogInformation($"Agent {dto.id} posting HDD metrics.");

            try
            {
                HDDMetricsRepository<MySqlConnection> repo = new HDDMetricsRepository<MySqlConnection>(_connector);

                repo.Create(dto);

                _logger.LogInformation($"Agent {dto.id} succesfully posted HDD metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
