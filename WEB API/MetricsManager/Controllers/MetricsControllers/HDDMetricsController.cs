using DTOs;
using MetricsAgent.Repository;
using MetricsEntetiesAndFunctions.Entities;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/manage/hdd")]
    [ApiController]
    public class HDDMetricsController : IBaseMetricManagerController<HDDMetricsDTO, MyDbContext>
    {
        public HDDMetricsController(ILogger<HDDMetricsController> logger, MyDbContext dbContext) : base(logger, dbContext)
        {
            _logger.LogDebug(1, "HDD Manager Metrics Controller.");
        }
        [HttpGet("agent/{id}")]
        public override List<HDDMetricsDTO> GetAllMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Manager getting HDD metrics from agent {id}");

            try
            {
                HDDMetricsRepository<MyDbContext> repo = new HDDMetricsRepository<MyDbContext>(_dbContext);

                List<HDDMetricsDTO> metrics = repo.GetAllByAgent(id);

                _logger.LogInformation($"Manager sucessfully got HDD metrics from agent {id}");

                return metrics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }

        [HttpGet("agent/{id}/from/{fromTime}/to/{toTime}")]
        public override HDDMetricsDTO GetMetricsFromAgent([FromRoute] int id, [FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting HDD metrics from agent {id}. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                HDDMetricsRepository<MyDbContext> repo = new HDDMetricsRepository<MyDbContext>(_dbContext);

                HDDMetricsDTO dto = repo.GetByTimePeriod(from, to, id);

                _logger.LogInformation($"Manager succesfully got HDD metrics from agent {id}.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override List<HDDMetricsDTO> GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting CPU metrics from all agents. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                HDDMetricsRepository<MyDbContext> repo = new HDDMetricsRepository<MyDbContext>(_dbContext);

                List<HDDMetricsDTO> metrics = repo.GetByTimePeriod(from, to);

                _logger.LogInformation($"Manager succesfully got CPU metrics from all agents.");

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
