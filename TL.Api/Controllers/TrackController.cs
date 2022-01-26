using Microsoft.AspNetCore.Mvc;
using TL.Api.TrackDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]

public class TrackController : Controller
{
    private readonly ITrackRepository _trackRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TrackController(ITrackRepository trackRepository, IUnitOfWork unitOfWork)
    {
        _trackRepository = trackRepository;
        _unitOfWork = unitOfWork;
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTrackDTO>> FindTrack(int id)
    {
        var track = await _trackRepository.FindAsync(id);
        var returned = GetTrackDTO.FromTrack(track);
        return Ok(returned);
    }

    [HttpPost]
    public async Task<ActionResult> AddTrack([FromBody] PostTrackDTO dto)
    {
        var track = PostTrackDTO.ToTrack(dto);
        await _trackRepository.AddTrack(track);
        return Ok();
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
        var track = await _trackRepository.FindAsync(id);
        await _trackRepository.DeleteTrack(track.Id);
        return NoContent();
    }

    [HttpGet("{title}")]
    public async Task<ActionResult<IEnumerable<GetTracksDTO>>> FindByTitle(string title)
    {
        var tracks = await _trackRepository.FindByExactTitle(title);
        var returned = GetTracksDTO.GetAll(tracks);
        return Ok(returned);
    }

    [HttpGet("{tuneId}")]
    public async Task<ActionResult<IEnumerable<GetTracksDTO>>> FindByTune(int tuneId)
    {
        var tracks = await _unitOfWork.FindTracksByTune(tuneId);
        var returned = GetTracksDTO.GetAll(tracks);
        return Ok(returned);
    }
    
}