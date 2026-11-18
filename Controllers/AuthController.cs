using Microsoft.AspNetCore.Mvc;
using JobApplicationTracker.Api.DTOs;
using JobApplicationTracker.Api.Services.Interfaces;

namespace JobApplicationTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns 400 Bad Request
            }

            try
            {
                var result = await _authService.RegisterAsync(request);
                return StatusCode(201, result); // Returns 201 Created
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message }); // Returns 409 Conflict for duplicate email
            }
        }
    }
}