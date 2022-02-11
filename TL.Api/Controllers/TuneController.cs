using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.CQRS.TuneCQ.Commands;
using TL.Api.CQRS.TuneCQ.Queries;
using TL.Api.DTOs.TrackDTOs;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]

public class TuneController : Controller
{
    private readonly ITuneRepository _tuneRepository;
    private readonly ITuneTrackService _tuneTrackService;
    private readonly IMediator _mediator;

    public TuneController(ITuneRepository tuneRepository, ITuneTrackService tuneTrackService, IMediator mediator)
    {
        _tuneRepository = tuneRepository;
        _tuneTrackService = tuneTrackService;
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindTune(int id)
    {
        var query = new GetTuneByIdQuery(id);
        var result = await _mediator.Send(query);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("type/{type}")]
    public async Task<ActionResult> FindByType(TuneTypeEnum type)
    {
        var query = new GetTunesByTypeQuery(type);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("key/{key}")]
    public async Task<ActionResult> FindByKey(TuneKeyEnum key)
    {
        var query = new GetTunesByKeyQuery(key);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("type/{type}/key/{key}")]
    public async Task<ActionResult> FindByTypeAndKey(TuneTypeEnum type, TuneKeyEnum key)
    {
        var query = new GetTunesByTypeKeyQuery(type, key);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllTunes()
    {
        var query = new GetAllTunesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> AddTune([FromBody] PostTuneDTO dto)
    {
        var title = TuneTitle.Create(dto.Title);
        var composer = TuneComposer.Create(dto.Composer);
        var type = dto.Type;
        var key = dto.Key;
        var command = new CreateTuneCommand(title, composer, type, key);
        var result = await _mediator.Send(command);
        // return CreatedAtAction(nameof(FindTune), new {id = result.Id}, result);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutTune(int id, [FromBody] PutTuneDTO dto)
    {
        var title = TuneTitle.Create(dto.Title);
        var composer = TuneComposer.Create(dto.Composer);
        var command = new UpdateTuneCommand(id, title, composer, dto.Type, dto.Key);
        var result = await _mediator.Send(command);
        // return CreatedAtAction(nameof(FindTune), new {id = result.Id}, result);
        return Ok(result);

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
        var command = new RemoveAlternateTitleCommand(id, title);
        var result = _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet("{tuneId}/recordings")]
    public async Task<ActionResult> FindTuneRecordings(int tuneId)
    {
        var query = new GetTuneRecordingsQuery(tuneId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    
    
}