namespace Domain.Interfaces.Services
{
    public interface IQuestionService
    {
        Task<bool> WasAllQuestionsCorrectlyAnswered(Guid publicUnityId, Guid publicUserId);
    }
}