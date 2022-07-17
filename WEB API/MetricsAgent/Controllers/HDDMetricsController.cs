using DTOs;
using MetricsAgent.Repository;
using MetricsEntetiesAndFunctions.Entities;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Collections.Concurrent;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : BaseAgentController<HDDMetricsDTO, MyDbContext>
    {
        public HDDMetricsController(ILogger<HDDMetricsController> logger, MyDbContext dbContext, List<HDDMetricsDTO> records, AgentInfo agentInfo) : base(logger, dbContext, records, agentInfo)
        {
            _logger.LogDebug(1, "HDD Agent Metrics Controller.");
            _records = records;
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public override List<HDDMetricsDTO> GetMetrics([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Agent {_agentInfo.id} getting HDD metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                HDDMetricsRepository repo = new HDDMetricsRepository(_dbContext);

                DateTime date = DateTime.Now - new TimeSpan(0, 0, 10, 0);

                if (from >= date && _records.Count != 0)
                {
                    List<HDDMetricsDTO> dtos = repo.GetByTimePeriod(from, to, _records);

                    _logger.LogInformation($"Agent {_agentInfo.id} succesfully got HDD metrics.");

                    return dtos;
                }

                List<HDDMetricsDTO> dto = repo.GetByTimePeriod(from, to, _agentInfo.id);

                _logger.LogInformation($"Agent {_agentInfo.id} succesfully got HDD metrics");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpPost("post")]
        public override void PostMetrics([FromBody] List<HDDMetricsDTO> metrics)
        {
            _logger.LogInformation($"Agent {_agentInfo.id} posting HDD metrics.");

            try
            {
                HDDMetricsRepository repo = new HDDMetricsRepository(_dbContext);

                int count = metrics.Count;

                foreach (HDDMetricsDTO metric in metrics)
                    repo.Create(metric);

                _logger.LogInformation($"Agent {_agentInfo.id} succesfully posted HDD metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
