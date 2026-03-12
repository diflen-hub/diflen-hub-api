using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infra.Services
{
    public class QuestionService(IQuestionRepository questionRepository, IAnswerRepository answerRepository) : IQuestionService
    {
        public async Task<bool> WasAllQuestionsCorrectlyAnswered(Guid publicUnityId, Guid publicUserId)
        {
            var questionsFromUnity = await questionRepository.GetListAsync(q => q.Unity.PublicId == publicUnityId);
            var questionIds = questionsFromUnity.Select(q => q.PublicId).ToList();

            var unityAnswersFromUser = await answerRepository.GetListAsync(a => a.Unity.PublicId == publicUnityId && a.User.PublicId == publicUserId);

            foreach (var questionId in questionIds)
            {
                if (!unityAnswersFromUser.Any(a => a.Question.PublicId == questionId && a.IsCorrect))
                {
                    return false;
                }
            }

            return true;
        }
    }
}