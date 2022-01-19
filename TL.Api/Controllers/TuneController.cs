using Microsoft.AspNetCore.Mvc;
using TL.Api.TuneDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/Tunes")]

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

    [HttpGet("{type}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByType(TuneTypeEnum type)
    {
        var tunes = await _tuneRepository.SortByTuneTypeAsync(type);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("{key}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByKey(TuneKeyEnum key)
    {
        var tunes = await _tuneRepository.SortByTuneKeyAsync(key);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("{type}/{key}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByTypeAndKey(TuneTypeEnum type, TuneKeyEnum key)
    {
        var tunes = await _tuneRepository.SortByTypeAndKeyAsync(type, key);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("{composer}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByComposer(string composer)
    {
        var tunes = await _tuneRepository.SortByExactComposerAsync(composer);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("{title}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByTitle(string title)
    {
        var tunes = await _tuneRepository.GetByTitle(title);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindAll()
    {
        var tunes = await _tuneRepository.GetAllTunes();
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpPost]
    public async Task<ActionResult> AddTune([FromBody] PostTuneDTO dto)
    {
        var tune = PostTuneDTO.ToTune(dto);
        await _tuneRepository.Add(tune);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutTune(int id, [FromBody] PutTuneDTO dto)
    {
        var tune = await _tuneRepository.FindAsync(id);
        var updated = PutTuneDTO.UpdatedTune(tune, dto);
        await _tuneRepository.Update(tune.Id);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTune(int id)
    {
        var tune = _tuneRepository.FindAsync(id);
        await _tuneRepository.Delete(tune.Id);
        return Ok();

    }
    
}