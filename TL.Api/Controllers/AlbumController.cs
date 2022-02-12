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
    var result = await _mediator.Send(query);
    return result == null ? NotFound() : Ok(GetAlbumDTO.FromAlbum(result));
  }
  
  [HttpGet]
  public async Task<IActionResult> FindAllAlbums()
  {
    var query = new GetAllAlbumsQuery();
    var result = await _mediator.Send(query);
    return Ok(result.Select(GetAlbumDTO.FromAlbum));
  }
  
  [HttpPost]
  public async Task<IActionResult> AddAlbum([FromBody] PostAlbumDTO dto)
  {
    var album = PostAlbumDTO.ToAlbum(dto);
    var command = new CreateAlbumCommand(album.Title, album.Year);
    var result = await _mediator.Send(command);
    return Ok(GetAlbumDTO.FromAlbum(result));
    
    // return CreatedAtAction(nameof(FindAlbum), new {id = album.Id}, returned);
  }
  
  [HttpPut("{id}")]
  public async Task<IActionResult> PutAlbum(int id, [FromBody] PutAlbumDTO dto)
  {
    var title = AlbumTitle.Create(dto.Title);
    var year = AlbumYear.Create(dto.Year);
    var command = new UpdateAlbumCommand(id, title, year);
    var result = await _mediator.Send(command);
    return Ok(GetAlbumDTO.FromAlbum(result));
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
  public async Task<IActionResult> AddNewArtistToAlbum(int albumId, [FromBody] PostArtistDTO dto)
  {
    var artist = PostArtistDTO.ToArtist(dto);
    var command = new AddNewArtistToAlbumCommand(albumId, artist.Name);
    var result = await _mediator.Send(command);
    return Ok(GetAlbumDTO.FromAlbum(result));
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