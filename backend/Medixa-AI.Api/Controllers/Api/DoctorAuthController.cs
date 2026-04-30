using Medixa_AI.Application.DTOs;
using Medixa_AI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medixa_AI.Api.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorAuthController : ControllerBase
    {
        private readonly IDoctorAuthService _doctorAuthService;

        public DoctorAuthController(IDoctorAuthService doctorAuthService)
        {
            _doctorAuthService = doctorAuthService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("Email is required.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Password is required.");

            var result = await _doctorAuthService.LoginAsync(dto);
            if (result == null)
                return Unauthorized("Invalid email or password.");

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(DoctorRegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                return BadRequest("FullName is required.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("Email is required.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Password is required.");

            if (dto.SpecializationID <= 0)
                return BadRequest("SpecializationID is required.");

            var result = await _doctorAuthService.RegisterAsync(dto);
            if (result == null)
                return BadRequest("Email already exists.");

            return Ok(result);
        }
    }
}
