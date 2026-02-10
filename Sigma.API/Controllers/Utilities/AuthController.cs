using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs;
using Sigma.Application.UseCases.Utilities;

namespace Sigma.API.Controllers.Utilities
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUserUseCase _useCase;

        public AuthController(LoginUserUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "NA";

            var result = await _useCase.ExecuteAsync(dto, ip);

            if (result == null)
                return Unauthorized("Invalid email or password");

            return Ok(result);
        }
    }
}
