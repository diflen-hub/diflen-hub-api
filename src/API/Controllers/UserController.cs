using Application.UseCases;
using Domain.Dtos;
using Domain.Dtos.Login;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController(IUserRepository userRepository, LoginUseCase loginUseCase, RegisterUseCase _useCase) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDtoIn registerDto)
        {
            var result = await _useCase.ExecuteAsync(registerDto.Email, registerDto.Username, registerDto.Password);
            return StatusCode((int)result.StatusCode, result.Content);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDtoIn loginDto)
        {
            var result = await loginUseCase.ExecuteAsync(loginDto.Email, loginDto.Password);
            return StatusCode((int)result.StatusCode, result.Content);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile([FromQuery] string username)
        {
            var user = await userRepository.GetAsync(u => u.Username == username);

            if (user is null) return NoContent();

            return Ok(new ProfileDtoOut
            {
                PublicId = user.PublicId,
                Experience = user.Experience,
                Username = user.Username,
                ProfilePic = $"data:{user.FileType};base64,{System.Text.Encoding.UTF8.GetString(user.ProfilePicture ?? [])}",
            });
        }
    }
}