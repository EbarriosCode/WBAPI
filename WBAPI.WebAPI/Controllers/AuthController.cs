using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WBAPI.Application.Common;
using WBAPI.Application.Features.Auth.Commands.LoginUser;
using WBAPI.Application.Features.Auth.Commands.RegisterUser;
using WBAPI.Application.Features.Auth.DTOs;

namespace WBAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ISender sender) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(BaseResponse<AuthResponseDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var result = await sender.Send(
                new RegisterUserCommand(dto.Username, dto.Email, dto.Password, dto.ConfirmPassword));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>login — devuelve JWT</summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(BaseResponse<AuthResponseDto>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await sender.Send(new LoginUserCommand(dto.Email, dto.Password));

            return result.Success ? Ok(result) : Unauthorized(result);
        }

        /// <summary>Endpoint protegido — muestra datos del token</summary>
        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var userId = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity?.Name;
            var roles = User.Claims
                .Where(c => c.Type ==
                    System.Security.Claims.ClaimTypes.Role)
                .Select(c => c.Value);

            return Ok(new { userId, username, roles, message = "Token válido" });
        }

    }
}
