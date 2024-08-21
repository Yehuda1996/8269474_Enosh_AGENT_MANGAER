using AgentsRest.Dto;
using AgentsRest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController(ITargetService targetService) : ControllerBase
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateAgent(TargetDto targetDto)
        {
            try
            {
                var newTarget = await targetService.CreateTargetAsync(targetDto);
                return Ok(newTarget.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetTarget(int id) =>
            Ok(await targetService.GetTargetByIdAsync(id));
    }
}
