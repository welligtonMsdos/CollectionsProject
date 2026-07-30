using Collection10Api.Application.Common;
using Collection10Api.Application.Dtos;
using Collection10Api.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collection10Api.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class VinylsController : ApiControllerBase
{
    private readonly IVinylService _vinylService;

    public VinylsController(IVinylService vinylService)
    {
        _vinylService = vinylService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VinylCreateDto vinylCreateDto)
    {
        var result = await _vinylService.CreateVinylAsync(vinylCreateDto, UserId);

        return CreatedAtAction(nameof(GetByGuid),
                               new { guid = result.Guid },
                               Result<VinylDto>.Ok(result, "Vinyl successfully created!"));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var vinyls = await _vinylService.GetVinylsAsync(UserId);

        return Ok(Result<IEnumerable<VinylDto>>.Ok(vinyls));
    }

    [HttpGet("ByCombo")]
    public async Task<IActionResult> GetByCombo()
    {
        var vinyls = await _vinylService.GetVinylByComboAsync(UserId);

        return Ok(Result<IEnumerable<VinylByComboDto>>.Ok(vinyls));
    }

    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        var vinyl = await _vinylService.GetVinylByGuidAsync(guid);

        if (vinyl is null) return NotFound(Result<object>.Failure("Vinil não encontrado."));

        return Ok(Result<VinylDto>.Ok(vinyl));
    }

    [HttpPut("{guid:guid}")]
    public async Task<IActionResult> Put(Guid guid, [FromBody] VinylUpdateDto vinylUpdateDto)
    {
        var updatedVinyl = await _vinylService.UpdateVinylAsync(guid, vinylUpdateDto, UserId);

        if (updatedVinyl is null) return NotFound(Result<object>.Failure("Vinil não encontrado para atualização."));

        return Ok(Result<VinylDto>.Ok(updatedVinyl, "Vinil atualizado com sucesso!"));
    }

    [HttpDelete("{guid:guid}")]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var deletedVinyl = await _vinylService.DeleteVinylAsync(guid);

        if (!deletedVinyl)
            return NotFound(Result<object>.Failure("Vinil não encontrado para remoção."));

        return Ok(Result<bool>.Ok(true, "Vinil removido com sucesso!"));
    }
}
