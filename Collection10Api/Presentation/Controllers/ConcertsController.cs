using Collection10Api.Application.Common;
using Collection10Api.Application.Dtos;
using Collection10Api.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collection10Api.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ConcertsController: ApiControllerBase
{
    private readonly IConcertService _concertService;

    public ConcertsController(IConcertService concertService)
    {
        _concertService = concertService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ConcertCreateDto concertCreateDto)
    {
        var result = await _concertService.CreateConcertAsync(concertCreateDto, UserId);

        return CreatedAtAction(nameof(GetByGuid),
                               new { guid = result.Guid },
                               Result<ConcertDto>.Ok(result, "Show criado com sucesso!"));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var concerts = await _concertService.GetConcertsAsync(UserId);

        return Ok(Result<IEnumerable<ConcertDto>>.Ok(concerts));
    }

    [HttpGet("Upcoming")]
    public async Task<IActionResult> GetUpcoming()
    {
        var concerts = await _concertService.GetUpcomingAsync(UserId);

        return Ok(Result<IEnumerable<ConcertDto>>.Ok(concerts));
    }

    [HttpGet("Past")]
    public async Task<IActionResult> GetPast()
    {
        var concerts = await _concertService.GetPastAsync(UserId);

        return Ok(Result<IEnumerable<ConcertDto>>.Ok(concerts));
    }

    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        var concert = await _concertService.GetConcertByGuidAsync(guid);

        if (concert is null)
            return NotFound(Result<object>.Failure("Show não encontrado."));

        return Ok(Result<ConcertDto>.Ok(concert));
    }

    [HttpPut("{guid:guid}")]
    public async Task<IActionResult> Put(Guid guid, [FromBody] ConcertUpdateDto concertUpdateDto)
    {
        var updatedConcert = await _concertService.UpdateConcertAsync(
                                                        guid,
                                                        concertUpdateDto,
                                                        UserId);

        if (updatedConcert is null) return NotFound(Result<object>.Failure("Show não encontrado para atualização."));

        return Ok(Result<ConcertDto>.Ok(updatedConcert, "Show atualizado com sucesso!"));
    }

    [HttpDelete("{guid:guid}")]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var deletedConcert = await _concertService.DeleteConcertAsync(guid);

        if (!deletedConcert) return NotFound(Result<object>.Failure("Show não encontrado para exclusão."));

        return Ok(Result<bool>.Ok(true, "Show removido com sucesso!"));
    }
}
