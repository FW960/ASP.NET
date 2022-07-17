using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Collections.Concurrent;
using System.Data.Common;

namespace MetricsAgent.Controllers
{

    [Route("metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : BaseAgentController<CPUMetricsDTO, MyDbContext>
    {
        public CPUMetricsController(ILogger<CPUMetricsController> logger, MyDbContext dbContext, List<CPUMetricsDTO> records, AgentInfo agentInfo) : base(logger, dbContext, records, agentInfo)
        {
            _logger.LogDebug(1, "CPU Agent Metrics Controller.");
            _records = records;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public override List<CPUMetricsDTO> GetMetrics([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Agent {_agentInfo.id} getting CPU metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                CPUMetricsRepository repo = new CPUMetricsRepository(_dbContext);

                DateTime date = DateTime.Now - new TimeSpan(0, 0, 10, 0);

                if (from >= date && _records.Count != 0)
                {
                    List<CPUMetricsDTO> dtos = repo.GetByTimePeriod(from, to, _records);

                    _logger.LogInformation($"Agent {_agentInfo.id} succesfully got CPU metrics.");

                    return dtos;
                }

                List<CPUMetricsDTO> dto = repo.GetByTimePeriod(from, to, _agentInfo.id);

                _logger.LogInformation($"Agent {_agentInfo.id} succesfully got CPU metrics");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpPost("post")]
        public override void PostMetrics([FromBody] List<CPUMetricsDTO> metrics)
        {
            _logger.LogInformation($"Agent {_agentInfo.id} posting CPU metrics.");

            try
            {
                CPUMetricsRepository repo = new CPUMetricsRepository(_dbContext);

                int count = metrics.Count;

                foreach (CPUMetricsDTO metric in metrics)
                    repo.Create(metric);

                _logger.LogInformation($"Agent {_agentInfo.id} succesfully posted CPU metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
