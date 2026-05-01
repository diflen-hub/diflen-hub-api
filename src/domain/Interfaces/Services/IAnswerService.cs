using domain.Dtos.Publics;
using Domain.Dtos;

namespace Domain.Interfaces.Services
{
    public interface IAnswerService
    {
        Task<GetLastAnswersResponse> GetLastAnswersAsync(Guid publicUserId, Guid publicLessonId);
        Task<GetLastAnswersResponse?> VerifyAnswersAsync(Guid publicLessonId, string unityName, List<PublicAnswerDto> answers, Guid publicUserId, Guid publicUnityId);
    }
}