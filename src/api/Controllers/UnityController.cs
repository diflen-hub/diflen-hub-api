using System.Security.Claims;
using Application.Dtos;
using Application.UseCases;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/unity")]
[ApiController]
[Authorize]
public class UnityController(IUnityRepository unityRepository, GetUnityUseCase getUnityUseCase) : ControllerBase
{
    [EndpointSummary("Obter lista")]
    [EndpointDescription("Retorna todas as unidades")]
    [ProducesResponseType<List<UnityResponseDto>>(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<List<UnityResponseDto>> GetAll()
    {
        var unities = await unityRepository.GetListAsync(u => true);
        return unities.Select(unity => new UnityResponseDto
        {
            PublicId = unity.PublicId,
            Name = unity.Name,
            Description = unity.Description,
        }).ToList();
    }

    [EndpointSummary("Obter unidade")]
    [EndpointDescription("Retorna uma única unidade")]
    [ProducesResponseType<UnityResponseDto>(StatusCodes.Status200OK, Description = "Quando a unidade é encontrada.")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Description = "Quando a unidade não é encontrada.")]
    [HttpGet("{unityName}")]
    public async Task<ActionResult<UnityResponseDto>> Get(string unityName)
    {
        var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var result = await getUnityUseCase.ExecuteAsync(unityName, Guid.Parse(publicUserId));
        return StatusCode((int)result.StatusCode, result.Content);
    }
}
