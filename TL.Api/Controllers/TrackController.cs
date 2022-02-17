using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.CQRS.TrackCQ.Commands;
using TL.Api.CQRS.TrackCQ.Queries;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain.ValueObjects.TrackTuneValueObjects;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]

public class TrackController : Controller
{
    private readonly IMediator _mediator;

    public TrackController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> FindTrack(int id)
    {
        var query = new GetTrackByIdQuery(id);
        var track = await _mediator.Send(query);
        return track
            .Map(GetTrackDTO.FromTrack)
            .Some<IActionResult>(Ok)
            .None(NotFound);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTracks()
    {
        var query = new GetAllTracksQuery();
        var result = await _mediator.Send(query);
        return Ok(result.Select(GetTrackDTO.FromTrack));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTrack(int id, [FromBody] UpdateTrackCommand request)
    {
        request.Id = id;
        var track = await _mediator.Send(request);
        return track
            .Some(x =>
                x.Succ<IActionResult>(t => Ok(GetTrackDTO.FromTrack(t)))
                    .Fail(e =>
                    {
                        var errors = e.Select(x => x.Message).ToList();
                        return UnprocessableEntity(new {errors});
                    }))
            .None(NotFound);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrack(int id)
    {
        var command = new DeleteTrackCommand(id);
        var result = await _mediator.Send(command);
        return result
            .Some<IActionResult>(_ => NoContent())
            .None(NotFound);
    }
    

    [HttpDelete("{trackId}/tune/{tuneId}")]
    public async Task<IActionResult> RemoveTuneFromTrack(int trackId, int tuneId)
    {
        var command = new RemoveTrackTuneCommand(trackId, tuneId);
        var result = await _mediator.Send(command);
        return Ok(GetTrackDTO.FromTrack(result));
        
        //udpate GetTrackDTO to also include tunes on track
    }

    [HttpPost("{trackId}/tune/{tuneId}")]
    public async Task<IActionResult> CreateTrackTune(int trackId, int tuneId, [FromBody] AddTrackTuneCommand request)
    {
        request.TrackId = trackId;
        request.TuneId = tuneId;
        var trackTune = await _mediator.Send(request);
        return trackTune
            .Some(x =>
                x.Succ<IActionResult>(t => Ok())
                    .Fail(e =>
                    {
                        var errors = e.Select(x => x.Message).ToList();
                        return UnprocessableEntity(new {errors});
                    }))
            .None(NotFound);

    }
    
}