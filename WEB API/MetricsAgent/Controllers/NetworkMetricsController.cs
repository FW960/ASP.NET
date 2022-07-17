using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Collections.Concurrent;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/network")]
    [ApiController]
    public class NetworkMetricsController : BaseAgentController<NetworkMetricsDTO, MyDbContext>
    {
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, MyDbContext dbContext, List<NetworkMetricsDTO> records, AgentInfo agentInfo) : base(logger, dbContext, records, agentInfo)
        {
            _logger.LogDebug(1, "Network Agent Metrics Controller.");
            _records = records;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public override List<NetworkMetricsDTO> GetMetrics([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Agent {_agentInfo.id} getting Network metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                NetworkMetricsRepository repo = new NetworkMetricsRepository(_dbContext);

                DateTime date = DateTime.Now - new TimeSpan(0,0,10,0); 

                if (from >= date && _records.Count != 0)
                {
                    List<NetworkMetricsDTO> dtos = repo.GetByTimePeriod(from, to, _records);

                    _logger.LogInformation($"Agent {_agentInfo.id} succesfully got Network metrics.");

                    return dtos;
                }

                List<NetworkMetricsDTO> dto = repo.GetByTimePeriod(from, to, _agentInfo.id);

                _logger.LogInformation($"Agent {_agentInfo.id} succesfully got Network metrics");

                return dto;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
        [HttpPost("post")]
        public override void PostMetrics([FromBody] List<NetworkMetricsDTO> metrics)
        {
            _logger.LogInformation($"Agent {_agentInfo.id} posting Network metrics.");

            try
            {
                NetworkMetricsRepository repo = new NetworkMetricsRepository(_dbContext);

                int count = metrics.Count;

                foreach (NetworkMetricsDTO metric in metrics)
                    repo.Create(metric);


                _logger.LogInformation($"Agent {_agentInfo.id} succesfully posted Network metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
