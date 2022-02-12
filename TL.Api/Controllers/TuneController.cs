using CSharpFunctionalExtensions;
using LanguageExt;
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
    private readonly IMediator _mediator;

    public TuneController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindTune(int id)
    {
        var query = new GetTuneByIdQuery(id);
        var result = await _mediator.Send(query);
        return result == null ? NotFound() : Ok(GetTuneDTO.FromTune(result));
    }

    [HttpGet("type/{type}")]
    public async Task<IActionResult> FindByType(TuneTypeEnum type)
    {
        var query = new GetTunesByTypeQuery(type);
        var result = await _mediator.Send(query);
        return Ok(result.Select(GetTuneDTO.FromTune));
    }

    [HttpGet("key/{key}")]
    public async Task<IActionResult> FindByKey(TuneKeyEnum key)
    {
        var query = new GetTunesByKeyQuery(key);
        var result = await _mediator.Send(query);
        return Ok(result.Select(GetTuneDTO.FromTune));
    }

    [HttpGet("type/{type}/key/{key}")]
    public async Task<IActionResult> FindByTypeAndKey(TuneTypeEnum type, TuneKeyEnum key)
    {
        var query = new GetTunesByTypeKeyQuery(type, key);
        var result = await _mediator.Send(query);
        return Ok(result.Select(GetTuneDTO.FromTune));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTunes()
    {
        var query = new GetAllTunesQuery();
        var result = await _mediator.Send(query);
        return Ok(result.Select(GetTuneDTO.FromTune));
    }

    [HttpPost]
    public async Task<IActionResult> AddTune([FromBody] PostTuneDTO dto)
    {
        var title = TuneTitle.Create(dto.Title);
        var composer = TuneComposer.Create(dto.Composer);
        var type = dto.Type;
        var key = dto.Key;
        var command = new CreateTuneCommand(title, composer, type, key);
        var result = await _mediator.Send(command);
        // return CreatedAtAction(nameof(FindTune), new {id = result.Id}, result);
        return Ok(GetTuneDTO.FromTune(result));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTune(int id, [FromBody] PutTuneDTO dto)
    {
        var title = TuneTitle.Create(dto.Title);
        var composer = TuneComposer.Create(dto.Composer);
        var command = new UpdateTuneCommand(id, title, composer, dto.Type, dto.Key);
        var result = await _mediator.Send(command);
        // return CreatedAtAction(nameof(FindTune), new {id = result.Id}, result);
        return Ok(GetTuneDTO.FromTune(result));

    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTune(int id)
    {
        var command = new DeleteTuneCommand(id);
        await _mediator.Send(command);
        return Ok($"Tune with ID '{id}' was deleted");
    }

    [HttpPost("{id}/titles")]
    public async Task<IActionResult> AddAlternateTitle(int id, [FromBody] AltTitleDTO dto)
    {
        var title = TuneTitle.Create(dto.AlternateTitle);
        var command = new AddAlternateTitleCommand(id, title);
        var result = await _mediator.Send(command);
        return Ok(GetTuneDTO.FromTune(result));
    }
    
    [HttpDelete("{id}/titles")]
    public async Task<IActionResult> RemoveAlternateTitle(int id, [FromBody] AltTitleDTO dto)
    {
        var title = TuneTitle.Create(dto.AlternateTitle);
        var command = new RemoveAlternateTitleCommand(id, title);
        var result = await _mediator.Send(command);
        return Ok(GetTuneDTO.FromTune(result));
    }
    
    [HttpGet("{tuneId}/recordings")]
    public async Task<IActionResult> FindTuneRecordings(int tuneId)
    {
        var query = new GetTuneRecordingsQuery(tuneId);
        var result = await _mediator.Send(query);
        return Ok(result.Select(GetTrackDTO.FromTrack));
    }
    
    
    
}