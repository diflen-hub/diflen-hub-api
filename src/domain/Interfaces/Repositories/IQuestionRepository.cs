using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        public Task<IEnumerable<Question>> GetListAsync(int lessonId);
    }
}
