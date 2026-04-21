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
                .Include(c => c.Unity)
                .Include(c => c.User)
                .Where(c => c.User != null && c.User.PublicId == publicUserId)
                .ToListAsync();
        }
    }
}