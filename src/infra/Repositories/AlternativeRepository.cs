using domain.Interfaces.Repositories;
using domain.Models;

namespace infra.Repositories
{
    public class AlternativeRepository(AppDbContext context) : BaseRepository<Alternative>(context), IAlternativeRepository { }
}