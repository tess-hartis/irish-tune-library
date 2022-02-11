using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.CQRS.AlbumCQ.Queries;
using TL.Api.CQRS.AlbumCQ.Commands;
using TL.Api.CQRS.Track.Commands;
using TL.Api.DTOs.AlbumDTOs;
using TL.Api.DTOs.ArtistDTOs;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain.ValueObjects.AlbumValueObjects;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

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
  public async Task<ActionResult<GetAlbumDTO>> FindAlbum(int id)
  {
    var query = new GetAlbumByIdQuery(id);
    var result = await _mediator.Send(query);
    return result == null ? NotFound() : Ok(result);
  }
  
  [HttpGet]
  public async Task<ActionResult<IEnumerable<GetAlbumDTO>>> FindAllAlbums()
  {
    var query = new GetAllAlbumsQuery();
    var result = await _mediator.Send(query);
    return Ok(result);
  }
  
  [HttpPost]
  public async Task<ActionResult> AddAlbum([FromBody] PostAlbumDTO dto)
  {
    var album = PostAlbumDTO.ToAlbum(dto);
    var command = new CreateAlbumCommand(album.Title, album.Year);
    var result = await _mediator.Send(command);
    return Ok(result);
    
    // return CreatedAtAction(nameof(FindAlbum), new {id = album.Id}, returned);
  }
  
  [HttpPut("{id}")]
  public async Task<ActionResult> PutAlbum(int id, [FromBody] PutAlbumDTO dto)
  {
    var title = AlbumTitle.Create(dto.Title);
    var year = AlbumYear.Create(dto.Year);
    var command = new UpdateAlbumCommand(id, title, year);
    var result = await _mediator.Send(command);
    return Ok(result);
  }
  
  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteAlbum(int id)
  {
    var command = new DeleteAlbumCommand(id);
    await _mediator.Send(command);
    return Ok($"Album with ID '{id}' was deleted");
  }

  [HttpPost("{albumId}/artist/{artistId}")]
  public async Task<ActionResult> AddExistingArtistToAlbum(int albumId, int artistId)
  {
    var command = new AddExistingArtistToAlbumCommand(albumId, artistId);
    var result = await _mediator.Send(command);
    return Ok(result);
    
    //need to edit DTO to include album artists
  }

  [HttpPost("{albumId}/artist")]
  public async Task<ActionResult> AddNewArtistToAlbum(int albumId, [FromBody] PostArtistDTO dto)
  {
    var artist = PostArtistDTO.ToArtist(dto);
    var command = new AddNewArtistToAlbumCommand(albumId, artist.Name);
    var result = await _mediator.Send(command);
    return Ok(result);
  }

  [HttpDelete("{albumId}/artist/{artistId}")]
  public async Task<ActionResult> RemoveArtistFromAlbum(int albumId, int artistId)
  {
    var command = new RemoveArtistFromAlbumCommand(albumId, artistId);
    var result = await _mediator.Send(command);
    return Ok(result);
  }

  [HttpPost("{albumId}/track")]
  public async Task<ActionResult> AddTrackToAlbum(int albumId, [FromBody] PostTrackDTO dto)
  {
    var track = PostTrackDTO.ToTrack(dto);
    var command = new AddTrackToAlbumCommand(albumId, track.Title, track.TrackNumber);
    var result = await _mediator.Send(command);
    return Ok($"Track added to album: {result}");
  }

  // [HttpDelete("{albumId}/track/{trackId}")]
  // public async Task<ActionResult> RemoveTrackFromAlbum(int albumId, int trackId)
  // {
  //   await _albumTrackService.RemoveTrackFromAlbum(albumId, trackId);
  //   return Ok($"Track with ID '{trackId}' was removed from album with ID '{albumId}'");
  // }

  [HttpGet("{albumId}/tracks")]
  public async Task<ActionResult<IEnumerable<GetTrackDTO>>> GetAlbumTracks(int albumId)
  {
    var query = new GetAlbumTracksQuery(albumId);
    var result = await _mediator.Send(query);
    return Ok(result);
  }

  [HttpGet("{albumId}/artists")]
  public async Task<ActionResult<IEnumerable<GetArtistDTO>>> GetAlbumArtists(int albumId)
  {
    var query = new GetAlbumArtistsQuery(albumId);
    var result = await _mediator.Send(query);
    return Ok(result);
  }
}