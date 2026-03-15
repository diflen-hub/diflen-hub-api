using System.Security.Claims;
using api.Controllers.Requests;
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
        [EndpointSummary("Validar Respostas")]
        [EndpointDescription("Recebe uma lista de respostas, valida essas respostas, grava o histórico de tentativas e retorna o resultado")]
        [ProducesResponseType<GetLastAnswersOut>(StatusCodes.Status200OK, Description = "Quando as respostas foram validadas (independente se estavam corretas ou não).")]
        [ProducesResponseType<GetLastAnswersOut>(StatusCodes.Status400BadRequest, Description = "Quando é passado algum parâmetro ou Id incorreto nas respostas.")]
        [HttpPost("verify-answers")]
        public async Task<ActionResult<GetLastAnswersOut>> VerifyAnswers([FromBody] AnswerVerifyRequestDto answerVerifyIn)
        {
            var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await verifyAnswersUseCase.ExecuteAsync(answerVerifyIn, Guid.Parse(publicUserId));

            return StatusCode((int)result.StatusCode, result.Content);
        }
    }
}