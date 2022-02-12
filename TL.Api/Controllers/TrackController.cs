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
    private readonly ITrackRepository _trackRepository;
    private readonly ITuneTrackService _tuneTrackService;
    private readonly IMediator _mediator;

    public TrackController(ITrackRepository trackRepository, ITuneTrackService tuneTrackService, IMediator mediator)
    {
        _trackRepository = trackRepository;
        _tuneTrackService = tuneTrackService;
        _mediator = mediator;
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> FindTrack(int id)
    {
        var query = new GetTrackByIdQuery(id);
        var result = _mediator.Send(query);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTracks()
    {
        var query = new GetAllTracksQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTrack(int id, [FromBody] PutTrackDTO dto)
    {
        var title = TrackTitle.Create(dto.Title);
        var trackNumber = TrackNumber.Create(dto.Number);
        var command = new UpdateTrackCommand(id, title, trackNumber);
        var result = await _mediator.Send(command);
        return Ok(result);
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
        await _tuneTrackService.RemoveTuneFromTrack(trackId, tuneId);
        return Ok($"Tune with ID '{tuneId}' was removed from track with ID '{trackId}'");
    }

    [HttpPost("{trackId}/tune/{tuneId}")]
    public async Task<IActionResult> AddExistingTuneToTrack(int trackId, int tuneId, [FromBody] PostTuneToTrackDTO dto )
    {
        var order = TrackTuneOrder.Create(dto.Order);
        var command = new AddTrackTuneCommand(trackId, tuneId, order);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{trackId}/tunes")]
    public async Task<IActionResult> GetTunesOnTrack(int trackId)
    {
        var query = new GetTunesOnTrackQuery(trackId);
        var result = await _mediator.Send(query);
        return Ok(result);
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