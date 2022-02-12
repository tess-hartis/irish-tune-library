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
        var result = await _mediator.Send(query);
        return result == null ? NotFound() : Ok(GetTrackDTO.FromTrack(result));
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
        return track.Match<IActionResult>(
            t => Ok(GetTrackDTO.FromTrack(t)),
            e =>
            {
                var errorList = e.Select(e => e.Message).ToList();
                return UnprocessableEntity(new {code = 422, errors = errorList});
            });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrack(int id)
    {
        var command = new DeleteTrackCommand(id);
        await _mediator.Send(command);
        return Ok($"Track with ID '{id}' was deleted");
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
    public async Task<IActionResult> AddExistingTuneToTrack(int trackId, int tuneId, [FromBody] PostTuneToTrackDTO dto )
    {
        var order = TrackTuneOrder.Create(dto.Order);
        var command = new AddTrackTuneCommand(trackId, tuneId, order);
        var result = await _mediator.Send(command);
        return Ok(GetTrackDTO.FromTrack(result));
    }

    [HttpGet("{trackId}/tunes")]
    public async Task<IActionResult> GetTunesOnTrack(int trackId)
    {
        var query = new GetTunesOnTrackQuery(trackId);
        var result = await _mediator.Send(query);
        return Ok(result.Select(GetTrackTuneDTO.FromTrackTune));
    }

    // [HttpPost]
    // public async Task<ActionResult> AddTrack([FromBody] PostTrackDTO dto)
    // {
    //     var track = PostTrackDTO.ToTrack(dto);
    //     track.SetAlbumId(1);
    //     await _trackRepository.AddAsync(track);
    //     return Ok();
    // }

}