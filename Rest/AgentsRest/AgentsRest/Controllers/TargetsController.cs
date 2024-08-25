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
    public class TargetsController(ITargetService targetService, IJwtService jwtService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateAgent([FromBody] TargetDto targetDto)
        {
            try
            {
                var newTarget = await targetService.CreateTargetAsync(targetDto);
                return Ok($"Id : {newTarget.Id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetTarget(int id) =>
            Ok(await targetService.GetTargetByIdAsync(id));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllTargets() =>
            Ok(await targetService.GetAllTargetsAsync());

        [HttpPut("{id}/pin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutInitialCoordinatesForTarget(int id, [FromBody] CoordinatesDto coordinatesDto)
        {
            try
            {
                await targetService.StartingCoordinatesForTargetByIdAsync(id, coordinatesDto);
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
        public async Task<ActionResult> MoveTarget(int id,[FromBody] MoveDto moveDto)
        {
            try
            {
                await targetService.MoveTargetById(id, moveDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
