using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDotNetCoreTemplate.Models.Dto;
using TestDotNetCoreTemplate.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestTemplate.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthRepository _authRepository;

        public AuthController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserCredentialsDto creds)
        {
            var user = await _authRepository.Login(creds);
            if (user != null)
            {
                var token = _authRepository.GenerateJwtToken(user);
                return Ok(new
                {
                    token = token,
                    user = new
                    {
                        id = user.Id,
                        username = user.UserName
                    }
                });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto userToRegister)
        {
            var result = await _authRepository.Register(userToRegister);
            if (result)
            {
                return Ok();
            }

            return StatusCode(403); // Frobidden
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Register(string userId)
        {
            var result = await _authRepository.Delete(userId);
            if (result)
            {
                return Ok();
            }

            return StatusCode(403); // Frobidden
        }

    }
}
