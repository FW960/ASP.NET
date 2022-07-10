using MetricsAgent.DTOs;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/ram")]
    [ApiController]
    public class RAMMetricsController : BaseAgentController<RAMMetricsDTO, MySqlConnection> 
    {
        public RAMMetricsController(ILogger<RAMMetricsController> logger, MySqlConnection connector) : base(logger, connector)
        {
            _logger.LogDebug(1, "RAM Agent Metrics Logger");
        }

        [HttpGet("agent/{id}/available/from/{fromTime}/to/{toTime}")]
        public override RAMMetricsDTO GetMetrics([FromRoute] string fromTime, [FromRoute]string toTime, [FromRoute]int id)
        {
            _logger.LogInformation($"Agent {id} getting RAM metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            RAMMetricsRepository<MySqlConnection> repo = new RAMMetricsRepository<MySqlConnection>(_connector);

            try
            {
                RAMMetricsDTO dto = repo.GetByTimePeriod(from, to, id);

                _logger.LogInformation($"Agent {id} succesfully got RAM metrics.");

                return dto;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
            
        }
        
        [HttpPost("post")]
        public override void PostMetrics(RAMMetricsDTO dto)
        {
            _logger.LogInformation($"Agent {dto.id} posting RAM metrics.");

            try
            {
                RAMMetricsRepository<MySqlConnection> repo = new RAMMetricsRepository<MySqlConnection>(_connector);

                repo.Create(dto);

                _logger.LogInformation($"Agent {dto.id} succesfully posted RAM metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
