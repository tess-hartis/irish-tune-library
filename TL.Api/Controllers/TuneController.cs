using MediatR;
using Microsoft.AspNetCore.Mvc;
using TL.Api.DTOs.TrackDTOs;
using TL.Api.DTOs.TuneDTOS;
using TL.CQRS.TuneCQ.Commands;
using TL.CQRS.TuneCQ.Queries;

namespace TL.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

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
        var tune = await _mediator.Send(query);
        return tune
            .Map(GetTuneDTO.FromTune)
            .Some<IActionResult>(Ok)
            .None(NotFound);
    }

    [HttpGet("type/{type}")]
    public async Task<IActionResult> FindByType(string type)
    {
        var query = new GetTunesByTypeQuery(type);
        var result = await _mediator.Send(query);
        return Ok(result.Select(GetTuneDTO.FromTune));
    }
    
    [HttpGet("key/{key}")]
    public async Task<IActionResult> FindByKey(string key)
    {
        var query = new GetTunesByKeyQuery(key);
        var result = await _mediator.Send(query);
        return Ok(result.Select(GetTuneDTO.FromTune));
    }
    
    [HttpGet("type/{type}/key/{key}")]
    public async Task<IActionResult> FindByTypeAndKey(string type, string key)
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
    public async Task<IActionResult> AddTune([FromBody] CreateTuneCommand request)
    {
        var tune = await _mediator.Send(request);
        return tune.Match<IActionResult>(
            t => Ok(),
            e =>
            {
                var errors = e.Select(e => e.Message).ToList();
                return UnprocessableEntity(new {errors});
            });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTune(int id, [FromBody] UpdateTuneCommand request)
    {
        request.Id = id;
        var tune = await _mediator.Send(request);
        return tune
            .Some(x =>
                x.Succ<IActionResult>(t => Ok())
                    .Fail(e =>
                    {
                        var errors = e.Select(x => x.Message).ToList();
                        return UnprocessableEntity(new {errors});
                    }))
            .None(NotFound);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTune(int id)
    {
        var command = new DeleteTuneCommand(id);
        var result = await _mediator.Send(command);
        return result
            .Some<IActionResult>(_ => NoContent())
            .None(NotFound);
    }

    [HttpPost("{id:int}/titles")]
    public async Task<IActionResult> AddAlternateTitle(int id, [FromBody] AddAlternateTitleCommand request)
    {
        request.Id = id;
        var result = await _mediator.Send(request);
        return result
            .Some(x =>
                x.Succ<IActionResult>(u => Ok())
                    .Fail(e =>
                    {
                        var errors = e.Select(x => x.Message).ToList();
                        return UnprocessableEntity(new {errors});
                    }))
            .None(NotFound);
    }
    
    [HttpDelete("{id}/titles")]
    public async Task<IActionResult> RemoveAlternateTitle(int id, [FromBody] RemoveAlternateTitleCommand request )
    {
        request.Id = id;
        var tune = await _mediator.Send(request);
        return tune
            .Some<IActionResult>(b =>
            {
                if (b)
                    return Ok();
                return BadRequest();
                
            })
            .None(NotFound);
    }
    
    [HttpGet("{tuneId}/recordings")]
    public async Task<IActionResult> FindTuneRecordings(int tuneId)
    {
        var query = new GetTuneRecordingsQuery(tuneId);
        var tracks = await _mediator.Send(query);
        return tracks
            .Some<IActionResult>(t => 
                Ok(t.Select(GetTrackDTO.FromTrack)))
            .None(NotFound);
    }
}