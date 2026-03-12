using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infra.Services
{
    public class CertificateService(ICertificateRepository certificateRepository) : ICertificateService
    {
        public async Task<bool> WasCertificateAlreadyIssued(Guid publicUserId, Guid publicUnityId)
        {
            var certificate = await certificateRepository.GetAsync(c => c.User!.PublicId == publicUserId && c.Unity!.PublicId == publicUnityId);
            return certificate is not null;
        }
    }
}