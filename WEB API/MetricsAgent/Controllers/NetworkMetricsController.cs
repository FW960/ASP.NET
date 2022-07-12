using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/network")]
    [ApiController]
    public class NetworkMetricsController : BaseAgentController<NetworkMetricsDTO, MyDbContext> 
    {
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, MyDbContext dbContext) : base(logger, dbContext)
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
                NetworkMetricsRepository<MyDbContext> repo = new NetworkMetricsRepository<MyDbContext>(_dbContext);

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
            _logger.LogInformation($"Agent {dto.agent_id} posting Network metrics.");

            try
            {
                NetworkMetricsRepository<MyDbContext> repo = new NetworkMetricsRepository<MyDbContext>(_dbContext);

                repo.Create(dto);

                _logger.LogInformation($"Agent {dto.agent_id} succesfully posted Network metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
