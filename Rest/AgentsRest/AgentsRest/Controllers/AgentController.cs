using AgentsRest.Dto;
using AgentsRest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController(IAgentService agentService) : ControllerBase
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateAgent(AgentDto agentDto)
        {
            try
            {
                var newAgent = await agentService.CreateAgentAsync(agentDto);
                return Ok(newAgent.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAgent(int id) =>
            Ok(await agentService.GetAgentByIdAsync(id));
    }
}
