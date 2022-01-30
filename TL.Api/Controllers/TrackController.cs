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

    [HttpPost]
    public async Task<ActionResult> AddTrack([FromBody] PostTrackDTO dto)
    {
        var track = PostTrackDTO.ToTrack(dto);
        await _trackRepository.AddAsync(track);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutTrack(int id, [FromBody] PutTrackDTO dto)
    {
        var track  = await _trackRepository.FindAsync(id);
        var updated = PutTrackDTO.UpdatedTrack(track, dto);
        if (!await _trackRepository.UpdateTrack(updated, dto.Title, dto.TrackNumber))
            return new BadRequestResult();
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTrack(int id)
    {
        var isDeleted = await _trackRepository.DeleteAsync(id);
        if (!isDeleted)
        {
            return NotFound($"Track with ID '{id}' was not found");
        }

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

    [HttpPost("{id}/tune")]
    public async Task<ActionResult> AddNewTuneToTrack(int id, [FromBody] PostTuneDTO dto)
    {
        await _tuneTrackService.AddNewTuneToTrack(id, dto.Title, dto.Composer, dto.Type, dto.Key);
        return Ok();
    }
}