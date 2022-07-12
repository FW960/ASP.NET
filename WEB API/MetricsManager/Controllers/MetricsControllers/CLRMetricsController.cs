using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Common;

namespace MetricsManager.Controllers.MetricsControllers
{
    [Route("metrics/manage/clr")]
    [ApiController]
    public class CLRMetricsController : IBaseMetricManagerController<CLRMetricsDTO, MyDbContext> 
    {
        public CLRMetricsController(ILogger<CLRMetricsController> logger, MyDbContext dbContext) : base(logger, dbContext)
        {
            _logger.LogDebug(1, "CLR Manager Metrics Controller.");
        }
        [HttpGet("agent/{id}")]
        public override List<CLRMetricsDTO> GetAllMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Manager getting CLR metrics from agent {id}");

            try
            {
                CLRMetricsRepository<MyDbContext> repo = new CLRMetricsRepository<MyDbContext>(_dbContext);

                List<CLRMetricsDTO> metrics = repo.GetAllByAgent(id);

                _logger.LogInformation($"Manager sucessfully got CLR metrics from agent {id}");

                return metrics;
            }catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }

        [HttpGet("agent/{id}/from/{fromTime}/to/{toTime}")]
        public override CLRMetricsDTO GetMetricsFromAgent([FromRoute] int id, [FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting CLR metrics from agent {id}. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                CLRMetricsRepository<MyDbContext> repo = new CLRMetricsRepository<MyDbContext>(_dbContext);

                CLRMetricsDTO dto = repo.GetByTimePeriod(from, to, id);

                _logger.LogInformation($"Manager succesfully got CLR metrics from agent {id}.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public override List<CLRMetricsDTO> GetMetricsFromAllCluster([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Manager getting CLR metrics from all agents. Time: from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                CLRMetricsRepository<MyDbContext> repo = new CLRMetricsRepository<MyDbContext>(_dbContext);

                List<CLRMetricsDTO> metrics = repo.GetByTimePeriod(from, to);

                _logger.LogInformation($"Manager succesfully got CLR metrics from all agents.");

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

