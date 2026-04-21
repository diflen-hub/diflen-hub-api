using domain.Dtos.Publics;
using Domain.Dtos;

namespace Domain.Interfaces.Services
{
    public interface IAnswerService
    {
        Task<GetLastAnswersOut> GetLastAnswersAsync(Guid publicUserId, Guid publicLessonId);
        Task<GetLastAnswersOut?> VerifyAnswersAsync(Guid publicLessonId, string unityName, List<PublicAnswerDto> answers, Guid publicUserId, Guid publicUnityId);
    }
}