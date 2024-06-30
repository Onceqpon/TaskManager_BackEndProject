using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebAPI.Configuration;
using WebAPI.Dto;
using JWT.Builder;
using JWT.Algorithms;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Extensions.Logging;
using Core.Entity;
using WebApi.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<UserEntity> _manager;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(UserManager<UserEntity> manager, ILogger<AuthenticationController> logger, IConfiguration configuration, JwtSettings jwtSettings)
        {
            _manager = manager;
            _logger = logger;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            var logged = await _manager.FindByNameAsync(user.LoginName);
            if (logged == null || !await _manager.CheckPasswordAsync(logged, user.Password))
            {
                return Unauthorized();
            }

            var token = CreateToken(logged);
            return Ok(new AuthenticationResponseDto { Token = $"Bearer {token}" });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new UserEntity
            {
                UserName = user.UserName,
                Email = user.Email
            };

            var result = await _manager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User created successfully" });
        }

        private string CreateToken(UserEntity user)
        {
            return JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
                .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
                .AddClaim(JwtRegisteredClaimNames.Gender, "male")
                .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
                .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds())
                .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
                .Audience(_jwtSettings.Audience)
                .Issuer(_jwtSettings.Issuer)
                .Encode();
        }
    }

    public class AuthenticationResponseDto
    {
        public string Token { get; set; }
    }
}
