using domain.Interfaces.Repositories;
using domain.Models;

namespace infra.Repositories
{
    public class AnswerRepository(AppDbContext context) : BaseRepository<Answer>(context), IAnswerRepository { }
}