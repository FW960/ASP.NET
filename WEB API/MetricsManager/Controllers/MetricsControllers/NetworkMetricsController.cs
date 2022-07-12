using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/manage/network")]
    [ApiController]
    public class NetworkMetricsController: IBaseMetricManagerController<NetworkMetricsDTO, MyDbContext> 
    {
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, MyDbContext dbContext) : base(logger, dbContext)
        {
            _logger.LogDebug(1, "Network Manager Metrics Controller.");
        }
        [HttpGet("agent/{id}")]
        public override List<NetworkMetricsDTO> GetAllMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Manager getting Network metrics from agent {id}");

            try
            {
                NetworkMetricsRepository<MyDbContext> repo = new NetworkMetricsRepository<MyDbContext>(_dbContext);

                List<NetworkMetricsDTO> metrics = repo.GetAllByAgent(id);

                _logger.LogInformation($"Manager sucessfully got Network metrics from agent {id}");

                return metrics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }

        [HttpGet("agent/{id}/from/{fromTime}/to/{toTime}")]
        public override NetworkMetricsDTO GetMetricsFromAgent([FromRoute] int id, [FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting Network metrics from agent {id}. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                NetworkMetricsRepository<MyDbContext> repo = new NetworkMetricsRepository<MyDbContext>(_dbContext);

                NetworkMetricsDTO dto = repo.GetByTimePeriod(from, to, id);

                _logger.LogInformation($"Manager succesfully got Network metrics from agent {id}.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override List<NetworkMetricsDTO> GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting Network metrics from all agents. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                NetworkMetricsRepository<MyDbContext> repo = new NetworkMetricsRepository<MyDbContext>(_dbContext);

                List<NetworkMetricsDTO> metrics = repo.GetByTimePeriod(from, to);

                _logger.LogInformation($"Manager succesfully got Network metrics from all agents.");

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

