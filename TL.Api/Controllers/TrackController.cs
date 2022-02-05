using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.TrackDTOs;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]

public class TrackController : Controller
{
    private readonly ITrackRepository _trackRepository;
    private readonly ITuneTrackService _tuneTrackService;

    public TrackController(ITrackRepository trackRepository, ITuneTrackService tuneTrackService)
    {
        _trackRepository = trackRepository;
        _tuneTrackService = tuneTrackService;
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTrackDTO>> FindTrack(int id)
    {
        var track = await _trackRepository.FindAsync(id);
        var returned = GetTrackDTO.FromTrack(track);
        return Ok(returned);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTrackDTO>>> GetAllTracks()
    {
        var tracks = await _trackRepository.GetEntities().ToListAsync();
        var returned = tracks.Select(GetTrackDTO.FromTrack);
        return Ok(returned);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutTrack(int id, [FromBody] PutTrackDTO dto)
    {
        var title = TrackTitle.Create(dto.Title);
        var trackNumber = TrackNumber.Create(dto.Number);
        await _trackRepository.UpdateTrack(id, title, trackNumber);
        return Ok($"Track with ID '{id}' was updated");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTrack(int id)
    {
        await _trackRepository.DeleteAsync(id);
        return Ok($"Track with ID '{id}' was deleted");
    }
    

    [HttpDelete("{trackId}/tune/{tuneId}")]
    public async Task<ActionResult> RemoveTuneFromTrack(int trackId, int tuneId)
    {
        await _tuneTrackService.RemoveTuneFromTrack(trackId, tuneId);
        return Ok($"Tune with ID '{tuneId}' was removed from track with ID '{trackId}'");
    }

    [HttpPost("{trackId}/tune/{tuneId}")]
    public async Task<ActionResult> AddExistingTuneToTrack(int trackId, int tuneId, [FromBody] PostTuneToTrackDTO dto )
    {
        await _tuneTrackService.AddExistingTuneToTrack(trackId, tuneId, dto.Order);
        return Ok($"Tune with ID '{tuneId}' was added to track with ID '{trackId}'");
    }

    [HttpGet("{trackId}/tunes")]
    public async Task<ActionResult<IEnumerable<GetTrackTuneDTO>>> GetTunesOnTrack(int trackId)
    {
        var tunesOnTrack = await _tuneTrackService.GetTrackTunes(trackId);
        var returned = tunesOnTrack.Select(GetTrackTuneDTO.FromTrackTune);
        return Ok(returned);
    }
    
}