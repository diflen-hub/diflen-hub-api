using System.Net;
using Application.Dtos;
using Domain.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class GetUnityUseCase(IUnityRepository unityRepository, ICertificateRepository certificateRepository, IQuestionService questionService)
    {
        public async Task<UseCaseResult<UnityDtoOut>> ExecuteAsync(string unityName, Guid publicUserId)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return new() { StatusCode = HttpStatusCode.NoContent };

            var certificate = await certificateRepository.GetAsync(c => c.User!.PublicId == publicUserId && c.Unity!.PublicId == unity.PublicId);

            return new()
            {
                Content = new UnityDtoOut
                {
                    PublicId = unity.PublicId,
                    Description = unity.Description,
                    Name = unity.Name,
                    WasCertificateAlreadyIssued = certificate is not null,
                    WasAllQuestionsCorrectlyAnswered = await questionService.WasAllQuestionsCorrectlyAnswered(unity.PublicId, publicUserId)
                }
            };
        }
    }
}