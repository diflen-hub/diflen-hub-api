using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.Dtos;
using Application.UseCases;
using Domain.Dtos;
using Domain.Dtos.Login;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/user")]
    [Description("Controller com os endpoints relacionados à login, criação de usuário e obtenção livre de usuário para mostrar no perfil")]
    [ApiController]
    public class UsersController(IUserRepository userRepository, LoginUseCase loginUseCase, RegisterUseCase _useCase) : ControllerBase
    {
        [EndpointSummary("Registro")]
        [EndpointDescription("Cria um novo usuário.")]
        [ProducesResponseType(StatusCodes.Status201Created, Description = "Usuário criado com sucesso.")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDtoIn registerDto)
        {
            var result = await _useCase.ExecuteAsync(registerDto.Email, registerDto.Username, registerDto.Password);
            return StatusCode((int)result.StatusCode, result.Content);
        }

        [EndpointSummary("Login")]
        [EndpointDescription("Realiza login na API para obter token JWT.")]
        [ProducesResponseType<UseCaseResult>(StatusCodes.Status200OK, Description = "Login realizado com sucesso.")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Description = "Quando a senha ou usuário está incorreto")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDtoIn loginDto)
        {
            var result = await loginUseCase.ExecuteAsync(loginDto.Email, loginDto.Password);
            return StatusCode((int)result.StatusCode, result.Content);
        }

        [EndpointSummary("Obter perfil")]
        [EndpointDescription("Retorna os dados de qualquer usuário solicitado.")]
        [ProducesResponseType(StatusCodes.Status200OK, Description = "Usuário encontrado.")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Description = "Usuário não encontrado.")]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile([FromQuery][Required][Description("Nome de usuário que deseja buscar.")] string username)
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