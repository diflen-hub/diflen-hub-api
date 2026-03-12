namespace Domain.Interfaces.Services
{
    public interface ICertificateService
    {
        Task<bool> WasCertificateAlreadyIssued(Guid publicUserId, Guid publicUnityId);
    }
}