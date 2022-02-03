using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.TrackDTOs;
using TL.Api.TuneDTOs;
using TL.Domain;
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
    public async Task<ActionResult<GetTracksDTO>> GetAllTracks()
    {
        var tracks = await _trackRepository.GetEntities().ToListAsync();
        var returned = tracks.Select(GetTrackDTO.FromTrack);
        return Ok(returned);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutTrack(int id, [FromBody] PutTrackDTO dto)
    {
        await _trackRepository.UpdateTrack(id, dto.Title, dto.TrackNumber);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTrack(int id)
    {
        await _trackRepository.DeleteAsync(id);
        return Ok();
    }
    
    [HttpGet("tune/{tuneId}")]
    public async Task<ActionResult<IEnumerable<GetTracksDTO>>> FindByTune(int tuneId)
    {
        var tracks = await _tuneTrackService.FindTracksByTune(tuneId);
        var returned = GetTracksDTO.GetAll(tracks);
        return Ok(returned);
    }

    [HttpDelete("{trackId}/tune/{tuneId}")]
    public async Task<ActionResult> RemoveTuneFromTrack(int trackId, int tuneId)
    {
        await _tuneTrackService.RemoveTuneFromTrack(trackId, tuneId);
        return Ok();
    }

    [HttpPost("{trackId}/tune/{tuneId}")]
    public async Task<ActionResult> AddExistingTuneToTrack(int trackId, int tuneId )
    {
        await _tuneTrackService.AddExistingTuneToTrack(trackId, tuneId);
        return Ok();
    }
    
}