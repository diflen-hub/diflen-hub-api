using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class AlternativeService(AppDbContext _context) : IAlternativeService
    {
        public async Task<Alternative?> GetCorrectAlternativeAsync(Guid publicQuestionId)
        {
            return await _context.Alternatives
            .Where(a => a.Question.PublicId == publicQuestionId && a.IsCorrect)
            .Include(a => a.Question)
            .FirstOrDefaultAsync();
        }
    }
}