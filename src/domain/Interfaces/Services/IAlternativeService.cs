using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IAlternativeService
    {
        public Task<Alternative?> GetCorrectAlternativeAsync(Guid publicQuestionId);
    }
}