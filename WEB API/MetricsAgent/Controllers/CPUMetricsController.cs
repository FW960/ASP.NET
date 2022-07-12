using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Controllers
{

    [Route("metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : BaseAgentController<CPUMetricsDTO, MyDbContext>
    {
        public CPUMetricsController(ILogger<CPUMetricsController> logger, MyDbContext dbContext) : base(logger, dbContext)
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
                CPUMetricsRepository<MyDbContext> repo = new CPUMetricsRepository<MyDbContext>(_dbContext);

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
            _logger.LogInformation($"Agent {dto.agent_id} posting CPU metrics.");

            try
            {
                CPUMetricsRepository<MyDbContext> repo = new CPUMetricsRepository<MyDbContext>(_dbContext);

                repo.Create(dto);

                _logger.LogInformation($"Agent {dto.agent_id} succesfully posted CPU metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
