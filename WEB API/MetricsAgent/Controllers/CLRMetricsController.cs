using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/clr")]
    public class CLRMetricsController : BaseAgentController<CLRMetricsDTO, MyDbContext>
    {
        public CLRMetricsController(ILogger<CLRMetricsController> logger, MyDbContext dbContext) : base(logger, dbContext)
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
                CLRMetricsRepository<MyDbContext> repo = new CLRMetricsRepository<MyDbContext>(_dbContext);

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
            _logger.LogInformation($"Agent {dto.agent_id} posting CLR metrics.");

            try
            {
                CLRMetricsRepository<MyDbContext> repo = new CLRMetricsRepository<MyDbContext>(_dbContext);

                repo.Create(dto);

                _logger.LogInformation($"Agent {dto.agent_id} succesfully posted CLR metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
