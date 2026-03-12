using Domain.Dtos;

namespace Domain.Interfaces.Services
{
    public interface IAnswerService
    {
        Task<GetLastAnswersOut> GetLastAnswersAsync(Guid publicUserId, Guid publicLessonId);
        Task<GetLastAnswersOut?> VerifyAnswersAsync(AnswerVerifyIn answerVerifyIn, Guid publicUserId, Guid publicUnityId);
    }
}