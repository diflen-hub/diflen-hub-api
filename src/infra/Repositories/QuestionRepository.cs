using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class QuestionRepository(AppDbContext context) : BaseRepository<Question>(context), IQuestionRepository
    {
        public async Task<IEnumerable<Question>> GetListAsync(int lessonId)
        {
            return await context.Questions
                .Include(q => q.Lesson)
                .Include(q => q.Alternatives)
                .Where(q => q.Lesson.Id == lessonId)
                .ToListAsync();
        }
    }
}