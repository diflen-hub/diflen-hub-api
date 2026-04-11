using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class CertificateRepository(AppDbContext context) : BaseRepository<Certificate>(context), ICertificateRepository
    {
        public async Task<List<Certificate>> GetCertificatesByUserId(Guid publicUserId)
        {
            return await context.Certificates
                .Where(c => c.PublicId == publicUserId)
                .Include(c => c.Unity)
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}