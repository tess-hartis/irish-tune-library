using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.TrackDTOs;
using TL.Api.TuneDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]

public class TuneController : Controller
{
    private readonly ITuneRepository _tuneRepository;
    private readonly ITuneTrackService _tuneTrackService;

    public TuneController(ITuneRepository tuneRepository, ITuneTrackService tuneTrackService)
    {
        _tuneRepository = tuneRepository;
        _tuneTrackService = tuneTrackService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTuneDTO>> FindTune(int id)
    {
        var tune = await _tuneRepository.FindAsync(id);
        var returned = GetTuneDTO.FromTune(tune);
        return Ok(returned);
    }

    [HttpGet("type/{type}")]
    public async Task<ActionResult<IEnumerable<GetTuneDTO>>> FindByType(TuneTypeEnum type)
    {
        var tunes = await _tuneRepository
            .GetByWhere(x => x.TuneType == type).ToListAsync();
        var returned = tunes.Select(GetTuneDTO.FromTune);
        return Ok();
    }

    [HttpGet("key/{key}")]
    public async Task<ActionResult<IEnumerable<GetTuneDTO>>> FindByKey(TuneKeyEnum key)
    {
        var tunes = await _tuneRepository
            .GetByWhere(x => x.TuneKey == key).ToListAsync();
        var returned = tunes.Select(GetTuneDTO.FromTune);
        return Ok(returned);
    }

    [HttpGet("type/{type}/key/{key}")]
    public async Task<ActionResult<IEnumerable<GetTuneDTO>>> FindByTypeAndKey(TuneTypeEnum type, TuneKeyEnum key)
    {
        var tunes = await _tuneRepository
            .GetByWhere(x => x.TuneType == type & x.TuneKey == key).ToListAsync();
        var returned = tunes.Select(GetTuneDTO.FromTune);
        return Ok(returned);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTuneDTO>>> GetAllTunes()
    {
        var tunes = await _tuneRepository.GetEntities().ToListAsync();
        var returned = tunes.Select(GetTuneDTO.FromTune);
        return Ok(returned);
    }

    [HttpPost]
    public async Task<ActionResult> AddTune([FromBody] PostTuneDTO dto)
    {
        var tune = PostTuneDTO.Create(dto);
        await _tuneRepository.AddAsync(tune);
        var returned = GetTuneDTO.FromTune(tune);
        return CreatedAtAction(nameof(FindTune),
            new {id = tune.Id}, returned);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutTune(int id, [FromBody] PutTuneDTO dto)
    {
        var title = TuneTitle.Create(dto.Title);
        await _tuneRepository.UpdateTune(id, title, dto.Composer, dto.Type, dto.Key);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTune(int id)
    { 
        await _tuneRepository.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("{id}/titles")]
    public async Task<ActionResult> AddAlternateTitle(int id, [FromBody] AltTitleDTO dto)
    {
        var title = TuneTitle.Create(dto.AlternateTitle);
        await _tuneRepository.AddAlternateTitle(id, title);
        return Ok();
    }
    
    [HttpDelete("{id}/titles")]
    public async Task<ActionResult> RemoveAlternateTitle(int id, [FromBody] AltTitleDTO dto)
    {
        var title = TuneTitle.Create(dto.AlternateTitle);
        await _tuneRepository.RemoveAlternateTitle(id, title);
        return Ok();
    }
    
    [HttpGet("{tuneId}/recordings")]
    public async Task<ActionResult<IEnumerable<GetTrackDTO>>> FindTuneRecordings(int tuneId)
    {
        var tracks = await _tuneTrackService.FindTracksByTune(tuneId);
        var returned = tracks.Select(GetTrackDTO.FromTrack);
        return Ok(returned);
    }
    
    
    
}