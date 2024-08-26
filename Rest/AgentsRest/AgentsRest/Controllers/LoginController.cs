using AgentsRest.Dto;
using AgentsRest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace AgentsRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IJwtService jwtService) : ControllerBase
    {
        private static readonly ImmutableList<string> _allowedNames = [
            "SimulationServer", "MVCServer"
        ];

        [HttpPost("auth")]
        public ActionResult<string> Login([FromBody] LoginDto loginDto) =>
            _allowedNames.Contains(loginDto.Name)
            ?Ok(jwtService.CreateToken(loginDto.Name))
            : BadRequest();
    }
}
