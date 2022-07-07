using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("[manage]")]
    [ApiController]
    public class AgentsController : Controller
    {
        private readonly AgentsInfoValuesHolder _holder;

        public AgentsController(AgentsInfoValuesHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agent)
        {
            if (!_holder.values.Contains(agent))
            {
                _holder.values.Add(agent);
            return Ok("Agent added.");
            }
            return BadRequest("Agent already exists.");
        }
        [HttpPost("agent/enable/{agentId}")]
        public IActionResult EnableAgent([FromQuery] int agentId)
        {
            return Ok();
        }
        [HttpPost("agent/disable/{agentId}")]
        public IActionResult DisableAgent([FromQuery] int agentId)
        {
            return Ok();
        }
        [HttpPost("agent/get/all")]
        public IActionResult GetAgents()
        {
            return Ok(_holder.values);
        }
    }
}
