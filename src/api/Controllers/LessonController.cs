using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Application.Dtos;
using Application.UseCases;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/lesson")]
    [ApiController]
    [Authorize]
    public class LessonController(GetLessonsUseCase getLessonsUseCase, GetLessonUseCase getLessonUseCase) : ControllerBase
    {
        [EndpointSummary("Obter Lista")]
        [EndpointDescription("Retorna uma lista de aulas com base no nome da unidade")]
        [HttpGet("list")]
        public async Task<ActionResult<List<LessonDtoOut>>> GetLessonsFromUnity([FromQuery][Required] string unityName)
        {
            var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await getLessonsUseCase.ExecuteAsync(unityName, Guid.Parse(publicUserId));

            return StatusCode((int)result.StatusCode, result.Content);
        }

        [EndpointSummary("Obter Lesson")]
        [EndpointDescription("Retorna uma Lesson baseada no seu PublicId")]
        [ProducesResponseType<LessonDtoOut>(StatusCodes.Status200OK, Description = "Quando a aula é encontrada.")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Description = "Quando a aula não foi encontrada.")]
        [HttpGet]
        public async Task<ActionResult<LessonDtoOut>> GetLesson([FromQuery] Guid publicLessonId)
        {
            var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await getLessonUseCase.ExecuteAsync(publicLessonId, Guid.Parse(publicUserId));

            return StatusCode((int)result.StatusCode, result.Content);
        }
    }
}
