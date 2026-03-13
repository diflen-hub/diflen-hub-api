using System.Net;
using Application.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Application.UseCases
{
    public class IssueCertificateUseCase(
        ICertificateService certificateService,
        IQuestionService questionService,
        ICertificateRepository certificateRepository,
        IUserRepository userRepository,
        IUnityRepository unityRepository)
    {
        public async Task<UseCaseResult<string>> ExecuteAsync(Guid publicUserId, string unityName)
        {
            var result = new UseCaseResult<string>();

            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            var user = await userRepository.GetAsyncNotNull(u => u.PublicId == publicUserId);

            if (unity is null) return new()
            {
                Content = "Unidade inválida.",
                StatusCode = HttpStatusCode.BadRequest
            };

            if (await certificateService.WasCertificateAlreadyIssued(publicUserId, unity.PublicId))
            {
                return new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = "O Certificado já havia sido emitido."
                };
            }

            if (await questionService.WasAllQuestionsCorrectlyAnswered(publicUserId, unity.PublicId))
            {
                var certificate = new Certificate
                {
                    CreatedAt = DateTime.Now,
                    UnityId = unity.Id,
                    UserId = user.Id
                };

                await certificateRepository.InsertAsync(certificate);
                
                result = new ()
                {
                    Content = "Certificado emitido com sucesso!",
                };
            }
            else
            {
                result = new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = "Nem todas as questões foram respondidas corretamente.",
                };
            }

            return result;
        }
    }
}