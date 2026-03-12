using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories
{
    public class AlternativeRepository(AppDbContext context) : BaseRepository<Alternative>(context), IAlternativeRepository { }
}