namespace Domain.Interfaces.Services
{
    public interface IQuestionService
    {
        Task<bool> WasUnityCorrectlyAnswered(Guid publicUnityId, Guid publicUserId);
    }
}