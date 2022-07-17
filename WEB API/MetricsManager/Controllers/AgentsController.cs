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
        public AgentInfo RegisterAgent([FromBody] string pcName)
        {
            _logger.LogInformation($"Agents controller creating new agent");
            try
            {
                AgentInfoRepository<MyDbContext> repo = new AgentInfoRepository<MyDbContext>(_dbContext);
                var agent = repo.Create(pcName);
                _logger.LogInformation($"Agents controller created {agent.id}");
                return agent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
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
