using System.Collections.Immutable;
using System.Security.Claims;
using Application.Dtos;
using Application.UseCases;
using Domain.Dtos;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/unity")]
[ApiController]
[Authorize]
public class UnityController(IUnityRepository unityRepository, GetUnityUseCase getUnityUseCase) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Obter lista")]
    [EndpointDescription("Retorna todas as unidades")]
    public async Task<UseCaseResult<List<UnityDtoOut>>> GetAll()
    {
        var unities = await unityRepository.GetListAsync(u => true);
        return new UseCaseResult<List<UnityDtoOut>>
        {
            Content = unities.Select(unity => new UnityDtoOut
            {
                PublicId = unity.PublicId,
                Name = unity.Name,
                Description = unity.Description,
            }).ToList()
        };
    }

    [HttpGet("{unityName}")]
    [EndpointSummary("Obter unidade")]
    [EndpointDescription("Retorna uma única unidade")]
    public async Task<ActionResult<UseCaseResult<UnityDtoOut>>> Get(string unityName)
    {
        var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var result = await getUnityUseCase.ExecuteAsync(unityName, Guid.Parse(publicUserId));
        return StatusCode((int)result.StatusCode, result.Content);
    }
}
