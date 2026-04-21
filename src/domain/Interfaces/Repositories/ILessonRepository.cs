using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface ILessonRepository : IBaseRepository<Lesson>
    {
        public Task<Lesson?> GetLesson(Guid publicId);
        public Task<Lesson?> GetLesson(string unityName, string publicId);
    }
}