using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;
using MySqlConnector;
using System.Data.Common;
using DTOs;
using MetricsEntetiesAndFunctions.Functions.Repository;
using MetricsEntetiesAndFunctions.Entities;

namespace MetricsManager.Controllers
{
    [Route("agents/manage")]
    [ApiController]
    public class AgentsController : Controller
    {
        private readonly MyDbContext _dbContext;

        private readonly ILogger<AgentsController> _logger;
        public AgentsController(MyDbContext dbContext, ILogger<AgentsController> logger)
        {
            _dbContext = dbContext;

            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent()
        {
            _logger.LogInformation($"Agents controller creating new agent");
            try
            {
                AgentInfoRepository<MyDbContext> repo = new AgentInfoRepository<MyDbContext>(_dbContext);
                var agent = repo.Create();
                _logger.LogInformation($"Agent created {agent.id}");
                return Ok($"Agents controller created agent {agent.id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
        [HttpPatch("agent/enable/{id}")]
        public IActionResult EnableAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Agents controller activating agent {id}");

            try
            {
                AgentInfoRepository<MyDbContext> repo = new AgentInfoRepository<MyDbContext>(_dbContext);

                repo.Enable(id);

                _logger.LogInformation($"Agents controller activated agent {id}");

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }

        }
        [HttpPatch("agent/disable/{id}")]
        public IActionResult DisableAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Agents controller deactivating agent {id}");

            try
            {
                AgentInfoRepository<MyDbContext> repo = new AgentInfoRepository<MyDbContext>(_dbContext);

                repo.Disable(id);

                _logger.LogInformation($"Agents controller deactivated agent {id}");

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet("agent/get/all")]
        public List<AgentInfoDTO> GetAgents()
        {
            _logger.LogInformation("Agents controller getting info about all agents");

            return new AgentInfoRepository<MyDbContext>(_dbContext).GetAll();
        }

        [HttpDelete("agent/delete/{id}")]
        public IActionResult DeleteAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Agents controller deleting agent {id}");

            try
            {
                AgentInfoRepository<MyDbContext> repo = new AgentInfoRepository<MyDbContext>(_dbContext);

                repo.Delete(id);

                _logger.LogInformation($"Agents controller succesfully deleted agent {id}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }

        }

    }

}
