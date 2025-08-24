using AuthService.Dtos.UserDtos;
using AuthService.Entities;
using AuthService.Mappers;
using AuthService.Repository;
using AuthService.Services;
using AuthService.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var existingUser = await _repository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return BadRequest("Email is already taken");
            existingUser = await _repository.GetByEmailAsync(registerDto.UserName);
            if (existingUser != null)
                return BadRequest("User Name is already taken");

            var user = registerDto.ToUserModel();
            user.Roles.Add("User");
            await _repository.CreateAsync(user);
            var jwtToken = _jwtService.GenerateToken(user);
            return Ok(new
            {
                User = user.ToUserDto(),
                Token = jwtToken
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _repository.GetByEmailAsync(loginDto.Email);
            if (user == null)
                return BadRequest("Invalid email or password");
            if (!Helpers.VerifyPassword(loginDto.Password, user.PasswordHash))
                return BadRequest("Invalid email or password");
            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                User = user.ToUserDto(),
                Token = token
            });
        }
    }
}
