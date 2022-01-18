using Microsoft.AspNetCore.Mvc;
using TL.Api.TrackDTOS;
using TL.Domain;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/Tracks")]

public class TrackController : Controller
{
    private readonly ITrackRepository _trackRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TrackController(ITrackRepository trackRepository, IUnitOfWork unitOfWork)
    {
        _trackRepository = trackRepository;
        _unitOfWork = unitOfWork;
    }

    // [HttpGet("{trackId}/album")]
    // public async Task<ActionResult<Album>> GetTrackAlbum(int trackId)
    // {
    //     var track = await _trackRepository.FindAsync(trackId);
    //     var album = await _unitOfWork.GetTrackAlbum(trackId);
    //     return album;
    // }

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
        await _trackRepository.Add(track);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutTrack(int id, [FromBody] PutTrackDTO dto)
    {
        var track = await _trackRepository.FindAsync(id);
        var updated = PutTrackDTO.UpdatedTrack(track, dto);
        var result = _trackRepository.Update(track.Id);
        if (!result.IsCompleted)
        {
            return new BadRequestResult();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTrack(int id)
    {
        var track = _trackRepository.FindAsync(id);
        if (track.IsCompleted)
        {
            await _trackRepository.Delete(track.Id);
            return new NoContentResult();
        }

        return new NotFoundResult();
    }

    [HttpGet("{title}")]
    public async Task<ActionResult<IEnumerable<GetTracksDTO>>> FindByTitle(string title)
    {
        var tracks = await _trackRepository.FindByExactTitleAsync(title);
        var returned = GetTracksDTO.GetTracks(tracks);
        return Ok(returned);
    }

    [HttpGet("{tuneId}")]
    public async Task<ActionResult<IEnumerable<GetTracksDTO>>> FindByTune(int tuneId)
    {
        var tracks = await _unitOfWork.GetByTuneFeatured(tuneId);
        var returned = GetTracksDTO.GetTracks(tracks);
        return Ok(returned);
    }
}