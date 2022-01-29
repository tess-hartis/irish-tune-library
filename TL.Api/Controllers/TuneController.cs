using Microsoft.AspNetCore.Mvc;
using TL.Api.TuneDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]

public class TuneController : Controller
{
    private readonly ITuneRepository _tuneRepository;

    public TuneController(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTuneDTO>> FindTune(int id)
    {
        var tune = await _tuneRepository.FindAsync(id);
        var returned = GetTuneDTO.FromTune(tune);
        return Ok(returned);
    }

    [HttpGet("type/{type}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByType(TuneTypeEnum type)
    {
        var tunes = await _tuneRepository.FindByType(type);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("key/{key}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByKey(TuneKeyEnum key)
    {
        var tunes = await _tuneRepository.FindByKey(key);
        var returned = GetTunesDTO.GetAll(tunes);

        if (returned.Count < 1)
            return NotFound("No tunes were found");

        return Ok(returned);
    }

    [HttpGet("typekey/{type}/{key}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByTypeAndKey(TuneTypeEnum type, TuneKeyEnum key)
    {
        var tunes = await _tuneRepository.FindByTypeAndKey(type, key);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> GetAllTunes()
    {
        var tunes = await _tuneRepository.GetAllTunes();
        var shortsyntax = tunes.Select(GetTuneDTO.FromTune);
        // var returned = GetTunesDTO.GetAll(tunes);
        return Ok(shortsyntax);
    }

    [HttpPost]
    public async Task<ActionResult> AddTune([FromBody] PostTuneDTO dto)
    {
        var tune = PostTuneDTO.Create(dto);
        await _tuneRepository.AddTune(tune);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutTune(int id, [FromBody] PutTuneDTO dto)
    {
        await _tuneRepository.UpdateTune(id, dto.Title, dto.Composer, dto.Type, dto.Key);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTune(int id)
    {
        var tune = await _tuneRepository.FindAsync(id);
        await _tuneRepository.DeleteTune(tune.Id);
        return Ok();

    }

    [HttpPost("{id}/titles")]
    public async Task<ActionResult> AddAlternateTitle(int id, [FromBody] AltTitleDTO dto)
    {
        var altTitle = dto.AlternateTitle;
        await _tuneRepository.AddAlternateTitle(id, altTitle);
        return Ok();
    }
    
    [HttpDelete("{id}/titles")]
    public async Task<ActionResult> RemoveAlternateTitle(int id, [FromBody] AltTitleDTO dto)
    {
        var title = dto.AlternateTitle;
        await _tuneRepository.RemoveAlternateTitle(id, title);
        return Ok();
    }
    
}