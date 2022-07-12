using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/ram")]
    [ApiController]
    public class RAMMetricsController : BaseAgentController<RAMMetricsDTO, MyDbContext> 
    {
        public RAMMetricsController(ILogger<RAMMetricsController> logger, MyDbContext dbContext) : base(logger, dbContext)
        {
            _logger.LogDebug(1, "RAM Agent Metrics Logger");
        }

        [HttpGet("agent/{id}/available/from/{fromTime}/to/{toTime}")]
        public override RAMMetricsDTO GetMetrics([FromRoute] string fromTime, [FromRoute]string toTime, [FromRoute]int id)
        {
            _logger.LogInformation($"Agent {id} getting RAM metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            RAMMetricsRepository<MyDbContext> repo = new RAMMetricsRepository<MyDbContext>(_dbContext);

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
            _logger.LogInformation($"Agent {dto.agent_id} posting RAM metrics.");

            try
            {
                RAMMetricsRepository<MyDbContext> repo = new RAMMetricsRepository<MyDbContext>(_dbContext);

                repo.Create(dto);

                _logger.LogInformation($"Agent {dto.agent_id} succesfully posted RAM metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
