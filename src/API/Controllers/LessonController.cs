using System.Security.Claims;
using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/lesson")]
    [ApiController]
    [Authorize]
    public class LessonController(GetLessonsUseCase getLessonsUseCase, GetLessonUseCase getLessonUseCase) : ControllerBase
    {
        [HttpGet("get-lessons-by-unity-name")]
        public async Task<IActionResult> GetLessonsFromUnity([FromQuery] string unityName)
        {
            var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await getLessonsUseCase.ExecuteAsync(unityName, Guid.Parse(publicUserId));

            return StatusCode((int)result.StatusCode, result.Content);
        }

        [HttpGet("get-lesson")]
        public async Task<IActionResult> GetLesson([FromQuery] string unityName, [FromQuery] Guid publicLessonId)
        {
            var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await getLessonUseCase.ExecuteAsync(unityName, publicLessonId, Guid.Parse(publicUserId));

            return StatusCode((int)result.StatusCode, result.Content);
        }
    }
}
