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
        var errors = e.Select(e => e.Message).ToList();
        return UnprocessableEntity(new {errors});
      });
  }
  
  [HttpPut("{id}")]
  public async Task<IActionResult> PutAlbum(int id, [FromBody] UpdateAlbumCommand request)
  {
    request.AlbumId = id;
    var album = await _mediator.Send(request);
    return album
      .Some(x =>
        x.Succ<IActionResult>(a => Ok(GetAlbumDTO.FromAlbum(a)))
          .Fail(e =>
          {
            var errors = e.Select(x => x.Message).ToList();
            return UnprocessableEntity(new {errors});

          }))
      .None(NotFound);
  }
  
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteAlbum(int id)
  {
    var command = new DeleteAlbumCommand(id);
    var result = await _mediator.Send(command);
    return result
      .Some<IActionResult>(_ => NoContent())
      .None(NotFound);
    
  }

  [HttpPost("{albumId}/artist/{artistId}")]
  public async Task<IActionResult> AddExistingArtistToAlbum(int albumId, int artistId)
  {
    var command = new AddExistingArtistToAlbumCommand(albumId, artistId);
    var album = await _mediator.Send(command);
    return album
      .Some<IActionResult>(a => Ok(GetAlbumDTO.FromAlbum(a)))
      .None(NotFound);

    //need to edit DTO to include album artists
  }

  [HttpDelete("{albumId}/artist/{artistId}")]
  public async Task<IActionResult> RemoveArtistFromAlbum(int albumId, int artistId)
  {
    var command = new RemoveArtistFromAlbumCommand(albumId, artistId);
    var album = await _mediator.Send(command);
    return album
      .Some<IActionResult>(b =>
      {
        if (b)
          return Ok();

        return BadRequest();
        
      })
      .None(NotFound);
  }

  [HttpPost("{albumId}/track")]
  public async Task<IActionResult> AddTrackToAlbum(int albumId, [FromBody] AddTrackToAlbumCommand request)
  {
    request.AlbumId = albumId;
    var track = await _mediator.Send(request);
    return track
      .Some<IActionResult>(x =>
        x.Succ<IActionResult>(t => Ok())
          .Fail(e =>
          {
            var errors = e.Select(x => x.Message).ToList();
            return UnprocessableEntity(new {errors});

          }))
      .None(NotFound);

  }

  [HttpGet("{albumId}/tracks")]
  public async Task<IActionResult> GetAlbumTracks(int albumId)
  {
    var query = new GetAlbumTracksQuery(albumId);
    var tracks = await _mediator.Send(query);
    return tracks
      .Some<IActionResult>(t =>
        Ok(t.Select(GetTrackDTO.FromTrack)))
      .None(NotFound);
  }

  [HttpGet("{albumId}/artists")]
  public async Task<IActionResult> GetAlbumArtists(int albumId)
  {
    var query = new GetAlbumArtistsQuery(albumId);
    var artists = await _mediator.Send(query);
    return artists
      .Some<IActionResult>(a =>
        Ok(a.Select(GetArtistDTO.FromArtist)))
      .None(NotFound);
  }
}