using domain.Interfaces.Repositories;
using domain.Models;

namespace infra.Repositories
{
    public class UnityRepository(AppDbContext context) : BaseRepository<Unity>(context), IUnityRepository { }
}