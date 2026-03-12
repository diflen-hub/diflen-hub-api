using System.Net;
using Application.Dtos;
using Domain.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class VerifyAnswersUseCase(IUnityRepository unityRepository, ILessonService lessonService, IAnswerService answerService, IQuestionService questionService, ICertificateService certificateService)
    {
        public async Task<UseCaseResult<GetLastAnswersOut>> ExecuteAsync(AnswerVerifyIn answerVerifyIn, Guid publicUserId)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == answerVerifyIn.UnityName);
            if (unity is null)
            {
                return new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "O nome da unidade está incorreto."
                };
            }

            var publicLessonId = answerVerifyIn.PublicLessonId;
            if (await lessonService.LessonAreAlreadyAnswered(publicUserId, publicLessonId))
            {
                return new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Todas as questões já foram respondidas",
                };
            }

            var verifiedAnswers = await answerService.VerifyAnswersAsync(answerVerifyIn, publicUserId, unity.PublicId);
            if (verifiedAnswers is null)
            {
                return new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Não foi possível encontrar uma das alternativas de alguma questão",
                };
            }

            verifiedAnswers.WasAllQuestionsCorrectlyAnswered = await questionService.WasAllQuestionsCorrectlyAnswered(unity.PublicId, publicUserId);
            verifiedAnswers.WasCertificateAlreadyIssued = await certificateService.WasCertificateAlreadyIssued(unity.PublicId, publicUserId);

            return new()
            {
                Content = verifiedAnswers,
            };
        }
    }
}