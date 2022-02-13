using MediatR;
using Microsoft.AspNetCore.Mvc;
using TL.Api.CQRS.AlbumCQ.Queries;
using TL.Api.CQRS.AlbumCQ.Commands;
using TL.Api.DTOs.AlbumDTOs;
using TL.Api.DTOs.ArtistDTOs;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain.ValueObjects.AlbumValueObjects;


namespace TL.Api.Controllers;

[Route("api/[controller]")]
public class AlbumController : Controller
{
  private readonly IMediator _mediator;

  public AlbumController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> FindAlbum(int id)
  {
    var query = new GetAlbumByIdQuery(id);
    var album = await _mediator.Send(query);
    return album
      .Map(GetAlbumDTO.FromAlbum)
      .Some<IActionResult>(Ok)
      .None(NotFound);
  }
  
  [HttpGet]
  public async Task<IActionResult> FindAllAlbums()
  {
    var query = new GetAllAlbumsQuery();
    var result = await _mediator.Send(query);
    return Ok(result.Select(GetAlbumDTO.FromAlbum));
  }
  
  [HttpPost]
  public async Task<IActionResult> AddAlbum([FromBody] CreateAlbumCommand request)
  {
    var album = await _mediator.Send(request);
    return album.Match<IActionResult>(
      a => Ok(GetAlbumDTO.FromAlbum(a)),
      e =>
      {
        var errorList = e.Select(e => e.Message).ToList();
        return UnprocessableEntity(new {code = 422, errors = errorList});
      });
  }
  
  [HttpPut("{id}")]
  public async Task<IActionResult> PutAlbum(int id, [FromBody] UpdateAlbumCommand request)
  {
    request.AlbumId = id;
    var album = await _mediator.Send(request);
    return album.Match<IActionResult>(
      a => Ok(GetAlbumDTO.FromAlbum(a)),
      e =>
      {
        var errorList = e.Select(e => e.Message).ToList();
        return UnprocessableEntity(new {code = 422, errors = errorList});
      });
  }
  
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteAlbum(int id)
  {
    var command = new DeleteAlbumCommand(id);
    await _mediator.Send(command);
    return Ok($"Album with ID '{id}' was deleted");
  }

  [HttpPost("{albumId}/artist/{artistId}")]
  public async Task<IActionResult> AddExistingArtistToAlbum(int albumId, int artistId)
  {
    var command = new AddExistingArtistToAlbumCommand(albumId, artistId);
    var result = await _mediator.Send(command);
    return Ok(GetAlbumDTO.FromAlbum(result));
    
    //need to edit DTO to include album artists
  }

  [HttpPost("{albumId}/artist")]
  public async Task<IActionResult> AddNewArtistToAlbum(int albumId, [FromBody] AddNewArtistToAlbumCommand request)
  {
    request.AlbumId = albumId;
    var artist = await _mediator.Send(request);
    return artist.Match<IActionResult>(
      a => Ok(GetArtistDTO.FromArtist(a)),
      e =>
      {
        var errorList = e.Select(e => e.Message).ToList();
        return UnprocessableEntity(new {code = 422, errors = errorList});
      });
  }

  [HttpDelete("{albumId}/artist/{artistId}")]
  public async Task<IActionResult> RemoveArtistFromAlbum(int albumId, int artistId)
  {
    var command = new RemoveArtistFromAlbumCommand(albumId, artistId);
    var result = await _mediator.Send(command);
    return Ok(GetAlbumDTO.FromAlbum(result));
  }

  [HttpPost("{albumId}/track")]
  public async Task<IActionResult> AddTrackToAlbum(int albumId, [FromBody] AddTrackToAlbumCommand request)
  {
    request.AlbumId = albumId;
    var track = await _mediator.Send(request);
    return track.Match<IActionResult>(
      t => Ok(GetTrackDTO.FromTrack(t)),
      e =>
      {
        var errorList = e.Select(e => e.Message).ToList();
        return UnprocessableEntity(new {code = 422, errors = errorList});
      });

  }

  // [HttpDelete("{albumId}/track/{trackId}")]
  // public async Task<ActionResult> RemoveTrackFromAlbum(int albumId, int trackId)
  // {
  //   await _albumTrackService.RemoveTrackFromAlbum(albumId, trackId);
  //   return Ok($"Track with ID '{trackId}' was removed from album with ID '{albumId}'");
  // }

  [HttpGet("{albumId}/tracks")]
  public async Task<IActionResult> GetAlbumTracks(int albumId)
  {
    var query = new GetAlbumTracksQuery(albumId);
    var result = await _mediator.Send(query);
    return Ok(result.Select(GetTrackDTO.FromTrack));
  }

  [HttpGet("{albumId}/artists")]
  public async Task<IActionResult> GetAlbumArtists(int albumId)
  {
    var query = new GetAlbumArtistsQuery(albumId);
    var result = await _mediator.Send(query);
    return Ok(result.Select(GetArtistDTO.FromArtist));
  }
}