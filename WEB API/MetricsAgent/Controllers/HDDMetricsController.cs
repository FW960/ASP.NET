using DTOs;
using MetricsAgent.Repository;
using MetricsEntetiesAndFunctions.Entities;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : BaseAgentController<HDDMetricsDTO, MyDbContext> 
    {
        public HDDMetricsController(ILogger<HDDMetricsController> logger, MyDbContext dbContext) : base(logger, dbContext)
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
                HDDMetricsRepository<MyDbContext> repo = new HDDMetricsRepository<MyDbContext>(_dbContext);

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
            _logger.LogInformation($"Agent {dto.agent_id} posting HDD metrics.");

            try
            {
                HDDMetricsRepository<MyDbContext> repo = new HDDMetricsRepository<MyDbContext>(_dbContext);

                repo.Create(dto);

                _logger.LogInformation($"Agent {dto.agent_id} succesfully posted HDD metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
