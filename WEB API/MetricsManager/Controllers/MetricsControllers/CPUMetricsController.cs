using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/manage/cpu")]
    [ApiController]
    public class CPUMetricsController : IBaseMetricManagerController<CPUMetricsDTO, MyDbContext>
    {
        public CPUMetricsController(ILogger<CPUMetricsController> logger, MyDbContext connector) : base(logger, connector)
        {
            _logger.LogDebug(1, "CPU Manager Metrics Controller.");
        }
        [HttpGet("agent/{id}")]
        public override List<CPUMetricsDTO> GetAllMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Manager getting CPU metrics from agent {id}");

            try
            {
                CPUMetricsRepository<MyDbContext> repo = new CPUMetricsRepository<MyDbContext>(_dbContext);

                List<CPUMetricsDTO> metrics = repo.GetAllByAgent(id);

                _logger.LogInformation($"Manager sucessfully got CPU metrics from agent {id}");

                return metrics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }

        [HttpGet("agent/{id}/from/{fromTime}/to/{toTime}")]
        public override CPUMetricsDTO GetMetricsFromAgent([FromRoute] int id, [FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting CPU metrics from agent {id}. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                CPUMetricsRepository<MyDbContext> repo = new CPUMetricsRepository<MyDbContext>(_dbContext);

                CPUMetricsDTO dto = repo.GetByTimePeriod(from, to, id);

                _logger.LogInformation($"Manager succesfully got CPU metrics from agent {id}.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override List<CPUMetricsDTO> GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting CPU metrics from all agents. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                CPUMetricsRepository<MyDbContext> repo = new CPUMetricsRepository<MyDbContext>(_dbContext);

                List<CPUMetricsDTO> metrics = repo.GetByTimePeriod(from, to);

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

