using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using api.Controllers.Requests;
using application.Dtos;
using Application.UseCases;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController(IUserRepository userRepository, LoginUseCase loginUseCase, RegisterUseCase _useCase) : ControllerBase
    {
        [EndpointSummary("Registro")]
        [EndpointDescription("Cria um novo usuário.")]
        [ProducesResponseType<string>(StatusCodes.Status201Created)]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterRequestDto registerDto)
        {
            var result = await _useCase.ExecuteAsync(registerDto.Email, registerDto.Username, registerDto.Password);
            return StatusCode((int)result.StatusCode, result.Content);
        }

        [EndpointSummary("Login")]
        [EndpointDescription("Realiza login na API para obter token JWT.")]
        [ProducesResponseType<LoginResponseDto>(StatusCodes.Status200OK, Description = "Quando o login é realizado com sucesso.")]
        [ProducesResponseType<LoginResponseDto>(StatusCodes.Status401Unauthorized, Description = "Quando a senha ou usuário estão incorretos.")]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginDto)
        {
            var result = await loginUseCase.ExecuteAsync(loginDto.Email, loginDto.Password);
            return StatusCode((int)result.StatusCode, result.Content);
        }

        [EndpointSummary("Obter perfil")]
        [EndpointDescription("Retorna os dados públicos de qualquer usuário solicitado.")]
        [ProducesResponseType<ProfileEntity>(StatusCodes.Status200OK, Description = "Quando o usuário é encontrado.")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Description = "Quando o usuário não é encontrado.")]
        [HttpGet("{username}")]
        public async Task<ActionResult<ProfileEntity>> Profile([FromRoute][Required][Description("Nome de usuário que deseja buscar.")] string username)
        {
            var user = await userRepository.GetAsync(u => u.Username == username);

            if (user is null) return NoContent();

            return Ok(new ProfileEntity
            {
                PublicId = user.PublicId,
                Experience = user.Experience,
                Username = user.Username,
                ProfilePic = $"data:{user.FileType};base64,{System.Text.Encoding.UTF8.GetString(user.ProfilePicture)}",
            });
        }
    }
}