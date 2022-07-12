using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using NLog;
using System.Data.Common;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/manage/ram")]
    [ApiController]
    public class RAMMetricsController : IBaseMetricManagerController<RAMMetricsDTO, MyDbContext>
    {
        public RAMMetricsController(ILogger<RAMMetricsController> logger, MyDbContext dbContext) : base(logger, dbContext)
        {
            _logger.LogDebug(1, "RAM Manager Metrics Controller.");
        }

        [HttpGet("agent/{id}")]
        public override List<RAMMetricsDTO> GetAllMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Manager getting RAM metrics from agent {id}");

            try
            {
                RAMMetricsRepository<MyDbContext> repo = new RAMMetricsRepository<MyDbContext>(_dbContext);

                List<RAMMetricsDTO> metrics = repo.GetAllByAgent(id);

                _logger.LogInformation($"Manager sucessfully got RAM metrics from agent {id}");

                return metrics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }

        [HttpGet("agent/{id}/from/{fromTime}/to/{toTime}")]
        public override RAMMetricsDTO GetMetricsFromAgent([FromRoute] int id, [FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting RAM metrics from agent {id}. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                RAMMetricsRepository<MyDbContext> repo = new RAMMetricsRepository<MyDbContext>(_dbContext);

                RAMMetricsDTO dto = repo.GetByTimePeriod(from, to, id);

                _logger.LogInformation($"Manager succesfully got RAM metrics from agent {id}.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override List<RAMMetricsDTO> GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting RAM metrics from all agents. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                RAMMetricsRepository<MyDbContext> repo = new RAMMetricsRepository<MyDbContext>(_dbContext);

                List<RAMMetricsDTO> metrics = repo.GetByTimePeriod(from, to);

                _logger.LogInformation($"Manager succesfully got RAM metrics from all agents.");

                return metrics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }

}

