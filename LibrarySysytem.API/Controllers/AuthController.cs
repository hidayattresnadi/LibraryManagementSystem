using LibrarySystem.Application.IServices;
using LibrarySystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysytem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]

        public async Task<IActionResult> RegisterAsync([FromBody] Register model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(model);

            if (result.Status == "Error")
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("login")]

        public async Task<IActionResult> LoginAsync([FromBody] Login model)
        {
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var result = await _authService.Login(model);

            if (result.Status == "Error")

                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPost("role")]
        public async Task<IActionResult> CreateRoleAsync([FromBody]string rolename)
        {
            var result = await _authService.CreateRoleAsync(rolename);
            return Ok(result);

        }
        [HttpPost("refresh-Token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshToken(request);
            return Ok(result);
        }
        [HttpPatch("Log-Out")]
        public async Task<IActionResult> LogoutAsync([FromBody]string email)
        {
            var response = await _authService.LogoutAsync(email);
            return Ok(response);
        }
    }
}
