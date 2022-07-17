using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Collections.Concurrent;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/ram")]
    [ApiController]
    public class RAMMetricsController : BaseAgentController<RAMMetricsDTO, MyDbContext>
    {
        public RAMMetricsController(ILogger<RAMMetricsController> logger, MyDbContext dbContext, List<RAMMetricsDTO> records, AgentInfo agentInfo) : base(logger, dbContext, records, agentInfo)
        {
            _logger.LogDebug(1, "RAM Agent Metrics Logger");
            _records = records;
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public override List<RAMMetricsDTO> GetMetrics([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Agent {_agentInfo.id} getting RAM metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                RAMMetricsRepository repo = new RAMMetricsRepository(_dbContext);

                DateTime date = DateTime.Now - new TimeSpan(0, 0, 10, 0);

                if (from >= date && _records.Count != 0)
                {
                    List<RAMMetricsDTO> dtos = repo.GetByTimePeriod(from, to, _records);

                    _logger.LogInformation($"Agent {_agentInfo.id} succesfully got RAM metrics.");

                    return dtos;
                }
                List<RAMMetricsDTO> dto = repo.GetByTimePeriod(from, to, _agentInfo.id);

                _logger.LogInformation($"Agent {_agentInfo.id} succesfully got RAM metrics.");


                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }

        }

        [HttpPost("post")]
        public override void PostMetrics([FromBody] List<RAMMetricsDTO> metrics)
        {
            _logger.LogInformation($"Agent {_agentInfo.id} posting RAM metrics.");

            try
            {
                RAMMetricsRepository repo = new RAMMetricsRepository(_dbContext);

                int count = metrics.Count;

                foreach (RAMMetricsDTO metric in metrics)
                    repo.Create(metric);

                _logger.LogInformation($"Agent {_agentInfo.id} succesfully posted RAM metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
