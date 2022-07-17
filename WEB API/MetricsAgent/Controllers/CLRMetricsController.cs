using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Collections.Concurrent;
using System.Data.Common;

namespace MetricsAgent.Controllers
{
    [Route("metrics/clr")]
    public class CLRMetricsController : BaseAgentController<CLRMetricsDTO, MyDbContext>
    {
        public CLRMetricsController(ILogger<CLRMetricsController> logger, MyDbContext dbContext, List<CLRMetricsDTO> records, AgentInfo agentInfo) : base(logger, dbContext, records, agentInfo)
        {
            _logger.LogDebug(1, "CLR Agent Metrics Controller");
            _records = records;
        }

        [HttpGet("allocated_heap_size/from/{fromTime}/to/{toTime}")]
        public override List<CLRMetricsDTO> GetMetrics([FromRoute] string fromTime, [FromRoute] string toTime)
        {
            _logger.LogInformation($"Agent {_agentInfo.id} getting CLR metrics from {fromTime} to {toTime}");

            DateTime from = DateTime.Parse(fromTime);

            DateTime to = DateTime.Parse(toTime);

            try
            {
                CLRMetricsRepository repo = new CLRMetricsRepository(_dbContext);

                DateTime date = DateTime.Now - new TimeSpan(0, 0, 10, 0);


                if (from >= date && _records.Count != 0)
                {
                    List<CLRMetricsDTO> dtos = repo.GetByTimePeriod(from, to, _records);

                    _logger.LogInformation($"Agent {_agentInfo.id} succesfully got CLR metrics.");

                    return dtos;
                }

                List<CLRMetricsDTO> dto = repo.GetByTimePeriod(from, to, _agentInfo.id);

                _logger.LogInformation($"Agent {_agentInfo.id} succesfully got CLR metrics");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }

        [HttpPost("post")]
        public override void PostMetrics([FromBody] List<CLRMetricsDTO> metrics)
        {
            _logger.LogInformation($"Agent {_agentInfo.id} posting CLR metrics.");

            try
            {
                CLRMetricsRepository repo = new CLRMetricsRepository(_dbContext);

                int count = metrics.Count;

                foreach (CLRMetricsDTO metric in metrics)
                    repo.Create(metric);

                _logger.LogInformation($"Agent {_agentInfo.id} succesfully posted CLR metrics");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw new Exception(ex.ToString());
            }
        }
    }
}
