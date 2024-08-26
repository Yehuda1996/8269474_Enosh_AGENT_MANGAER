using AgentsRest.Dto;
using AgentsRest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AgentsController(IAgentService agentService, IJwtService jwtService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateAgent([FromBody] AgentDto agentDto)
        {
            try
            {
                var newAgent = await agentService.CreateAgentAsync(agentDto);
                return Ok($"Id: {newAgent.Id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAgent(int id) =>
            Ok(await agentService.GetAgentByIdAsync(id));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllAgents() =>
            Ok(await agentService.GetAllAgentsAsync());

        [HttpPut("{id}/pin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutInitialCoordinatesForAgent(int id, [FromBody] CoordinatesDto coordinatesDto)
        {
            try
            {
                await agentService.StartingCoordinatesForAgentByIdAsync(id, coordinatesDto);
                return Ok();
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/move")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> MoveAgent(int id, [FromBody] MoveDto moveDto)
        {
            try
            {
                await agentService.MoveAgentById(id, moveDto);
                return Ok();    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAgent(int id) =>
            Ok(agentService.DeleteAgentByIdAsync(id));
    }
}
