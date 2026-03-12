using System.Security.Claims;
using Application.Dtos;
using Application.UseCases;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/questionnaire")]
    [ApiController]
    [Authorize]
    public class QuestionnaireController(VerifyAnswersUseCase verifyAnswersUseCase) : ControllerBase
    {
        [HttpPost("verify-answers")]
        public async Task<ActionResult<UseCaseResult<GetLastAnswersOut>>> VerifyAnswers([FromBody] AnswerVerifyIn answerVerifyIn)
        {
            var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await verifyAnswersUseCase.ExecuteAsync(answerVerifyIn, Guid.Parse(publicUserId));

            return StatusCode((int)result.StatusCode, result.Content);
        }
    }
}