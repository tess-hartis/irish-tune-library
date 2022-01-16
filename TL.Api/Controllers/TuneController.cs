using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.JsonPatch;
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
    public async Task<ActionResult<GetTuneDto>> FindTune(int id)
    {
        var tune = await _tuneRepository.FindAsync(id);
        var returned = GetTuneDto.FromTune(tune);
        return Ok(returned);
    }

    [HttpGet("Type/{type}")]
    public async Task<ActionResult<IEnumerable<GetTunesDto>>> FindByType(TuneTypeEnum type)
    {
        var tunes = await _tuneRepository.SortByTuneTypeAsync(type);
        var returned = GetTunesDto.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("Key/{key}")]
    public async Task<ActionResult<IEnumerable<GetTunesDto>>> FindByKey(TuneKeyEnum key)
    {
        var tunes = await _tuneRepository.SortByTuneKeyAsync(key);
        var returned = GetTunesDto.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("TypeKey/{type}/{key}")]
    public async Task<ActionResult<IEnumerable<GetTunesDto>>> FindByTypeAndKey(TuneTypeEnum type, TuneKeyEnum key)
    {
        var tunes = await _tuneRepository.SortByTypeAndKeyAsync(type, key);
        var returned = GetTunesDto.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("Composer/{composer}")]
    public async Task<ActionResult<IEnumerable<GetTunesDto>>> FindByComposer(string composer)
    {
        var tunes = await _tuneRepository.SortByExactComposerAsync(composer);
        var returned = GetTunesDto.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet("Title/{title}")]
    public async Task<ActionResult<IEnumerable<GetTunesDto>>> FindByTitle(string title)
    {
        var tunes = await _tuneRepository.GetByTitle(title);
        var returned = GetTunesDto.GetAll(tunes);
        return Ok(returned);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTunesDto>>> FindAll()
    {
        var tunes = await _tuneRepository.GetAllTunes();
        var returned = GetTunesDto.GetAll(tunes);
        return Ok(returned);
    }

    [HttpPost]
    public async Task<ActionResult> AddTune([FromBody] PostTuneDto dto)
    {
        var tune = PostTuneDto.ToTune(dto);
        await _tuneRepository.Add(tune);
        return Ok();
    }
    
}