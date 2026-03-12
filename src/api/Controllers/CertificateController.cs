using System.Security.Claims;
using API.Controllers.Dtos;
using Application.Dtos;
using Application.UseCases;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/certificate")]
    [ApiController]
    [Authorize]

    public class CertificateController(
        IssueCertificateUseCase issueCertificateUseCase,
        ICertificateRepository certificateRepository) : ControllerBase
    {
        [HttpPost("issue")]
        public async Task<ActionResult<UseCaseResult<object>>> IssueNewCertificate([FromQuery] string unityName)
        {
            var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var result = await issueCertificateUseCase.ExecuteAsync(Guid.Parse(publicUserId), unityName);

            return StatusCode((int)result.StatusCode, result.Content);
        }

        [HttpGet("get-all")]
        public async Task<UseCaseResult<List<CertificateGetAllResponse>>> GetUserCertificates()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var certificates = await certificateRepository.GetCertificatesByUserId(int.Parse(userId));

            return new UseCaseResult<List<CertificateGetAllResponse>>
            {
                Content = certificates.Select(c => new CertificateGetAllResponse
                {
                    UnityName = c.Unity!.Name,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };
        }
    }
}