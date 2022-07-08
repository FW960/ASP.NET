using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;
using MySqlConnector;

namespace MetricsManager.Controllers
{
    [Route("[manage]")]
    [ApiController]
    public class AgentsController : Controller
    {
        private readonly AgentsInfoValuesHolder _holder;

        private readonly MySqlConnection _connection;

        private readonly ILogger<AgentsController> _logger;
        public AgentsController(AgentsInfoValuesHolder holder, MySqlConnection connection, ILogger<AgentsController> logger)
        {
            _holder = holder;

            _connection = connection;

            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfoDTO agent)
        {
            _logger.LogInformation($"Agents controller creating new agent {agent.AgentId}");
            try
            {
                AgentInfoRepository repo = new AgentInfoRepository(_connection);
                repo.Create(agent);
                _holder.values.Add(agent);
                _logger.LogInformation($"Agent created {agent.AgentId}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost("agent/enable/{agentId}")]
        public IActionResult EnableAgent([FromQuery] int agentId)
        {
            throw new NotImplementedException();
        }
        [HttpPost("agent/disable/{agentId}")]
        public IActionResult DisableAgent([FromQuery] int agentId)
        {
            throw new NotImplementedException();
        }
        [HttpPost("agent/get/all")]
        public IActionResult GetAgents()
        {
            _logger.LogInformation("Agents controller getting info about all agents");

            return Ok(_holder.values);
        }
    }


}
