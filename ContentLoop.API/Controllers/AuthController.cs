using ContentLoop.API.Dto.Auth.Get;
using ContentLoop.API.Dto.Auth.Post;
using ContentLoop.API.Mappers;
using ContentLoop.API.Services;
using ContentLoop.BLL.Interfaces;
using ContentLoop.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ContentLoop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly TokenManager _tokenManager;
        public AuthController(IAuthService authService, TokenManager tokenManager)
        {
            _authService = authService;
            _tokenManager = tokenManager;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        [ProducesResponseType(typeof(RegisterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                UserModel user = await _authService.RegisterAsync(dto.ToBll());
                string token = _tokenManager.GenerateJwt(user.Id, user.Role);

                return Ok(new { token });
            }
            catch (ArgumentException ex)
            {
                return Conflict(new { errors = new[] { ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errors = new[] { ex.Message } });
            }
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                UserModel user = await _authService.LoginAsync(request.Email, request.Password);
                string token = _tokenManager.GenerateJwt(user.Id, user.Role);

                return Ok(new { token });
            }
            catch (ArgumentException ex)
            {
                return Unauthorized(new { errors = new[] { ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errors = new[] { ex.Message } });
            }
        }

        [Authorize]
        [HttpGet("Me")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                string userIdClaim = UserContextHelper.GetUserId(User);

                if (userIdClaim is null || !Guid.TryParse(userIdClaim, out Guid userId))
                    return Unauthorized();

                UserModel user = await _authService.GetByIdAsync(userId);
                return Ok(user.ToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errors = new[] { ex.Message } });
            }
        }
    }
}
