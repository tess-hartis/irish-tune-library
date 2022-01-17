using Microsoft.AspNetCore.Mvc;
using TL.Api.Dtos;
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

    [HttpGet("Type/{type}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByType(TuneTypeEnum type)
    {
        var tunes = await _tuneRepository.SortByTuneTypeAsync(type);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("Key/{key}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByKey(TuneKeyEnum key)
    {
        var tunes = await _tuneRepository.SortByTuneKeyAsync(key);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("TypeKey/{type}/{key}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByTypeAndKey(TuneTypeEnum type, TuneKeyEnum key)
    {
        var tunes = await _tuneRepository.SortByTypeAndKeyAsync(type, key);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("Composer/{composer}")]
    public async Task<ActionResult<IEnumerable<GetTunesDTO>>> FindByComposer(string composer)
    {
        var tunes = await _tuneRepository.SortByExactComposerAsync(composer);
        var returned = GetTunesDTO.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("Title/{title}")]
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
        var result = _tuneRepository.Update(tune.Id);
        if (!result.IsCompleted)
        {
            return new BadRequestResult();
        }

        return Ok();
    }
    
}