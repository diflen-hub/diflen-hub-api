using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface ILessonRepository : IBaseRepository<Lesson>
    {
        public Task<Lesson?> GetLesson(Guid publicId);
    }
}