using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.AlbumDTOs;
using TL.Api.ArtistDTOs;
using TL.Api.TrackDTOs;
using TL.Domain.ValueObjects.AlbumValueObjects;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]
public class AlbumController : Controller
{
  private readonly IAlbumRepository _albumRepository;
  private readonly IAlbumArtistService _albumArtistService;
  private readonly IAlbumTrackService _albumTrackService;

  public AlbumController(IAlbumRepository albumRepository, IAlbumArtistService albumArtistService,
    IAlbumTrackService albumTrackService)
  {
    _albumRepository = albumRepository;
    _albumArtistService = albumArtistService;
    _albumTrackService = albumTrackService;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<GetAlbumDTO>> FindAlbum(int id)
  {
    var album = await _albumRepository.FindAsync(id);
    var returned = GetAlbumDTO.FromAlbum(album);
    return Ok(returned);
  }
  
  [HttpGet]
  public async Task<ActionResult<IEnumerable<GetAlbumDTO>>> FindAllAlbums()
  {
    var albums = await _albumRepository.GetEntities().ToListAsync();
    var returned = albums.Select(GetAlbumDTO.FromAlbum);
    return Ok(returned);
  }
  
  [HttpPost]
  public async Task<ActionResult> AddAlbum([FromBody] PostAlbumDTO dto)
  {
    var album = PostAlbumDTO.ToAlbum(dto);
    await _albumRepository.AddAsync(album);
    var returned = GetAlbumDTO.FromAlbum(album);
    return CreatedAtAction(nameof(FindAlbum), new {id = album.Id}, returned);
  }
  
  [HttpPut("{id}")]
  public async Task<ActionResult> PutAlbum(int id, [FromBody] PutAlbumDTO dto)
  {
    var title = AlbumTitle.Create(dto.Title);
    var year = AlbumYear.Create(dto.Year);
    await _albumRepository.UpdateAlbum(id, title, year);
    return Ok($"Album with ID '{id}' was updated");
  }
  
  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteAlbum(int id)
  {
    await _albumRepository.DeleteAsync(id);
    return Ok($"Album with ID '{id}' was deleted");
  }

  [HttpPost("{albumId}/artist/{artistId}")]
  public async Task<ActionResult> AddExistingArtistToAlbum(int albumId, int artistId)
  {
    await _albumArtistService.AddExistingArtistToAlbum(albumId, artistId);
    return Ok($"Artist with ID '{artistId} was added to album with ID '{albumId}'");
  }

  [HttpPost("{albumId}/artist")]
  public async Task<ActionResult> AddNewArtistToAlbum(int albumId, [FromBody] PostArtistDTO dto)
  {
    var name = ArtistName.Create(dto.Name);
    await _albumArtistService.AddNewArtistToAlbum(albumId, name);
    return Ok($"New artist '{dto.Name}' was added to album with ID '{albumId}'");
  }

  [HttpDelete("{albumId}/artist/{artistId}")]
  public async Task<ActionResult> RemoveArtistFromAlbum(int albumId, int artistId)
  {
    await _albumArtistService.RemoveArtistFromAlbum(albumId, artistId);
    return Ok($"Artist with ID '{artistId}' was removed from album with ID '{albumId}'");
  }

  [HttpPost("{albumId}/track")]
  public async Task<ActionResult> AddTrackToAlbum(int albumId, [FromBody] PostTrackDTO dto)
  {
    var title = TrackTitle.Create(dto.Title);
    await _albumTrackService.AddNewTrackToAlbum(albumId, title, dto.TrackNumber);
    return Ok("New track successfully added");
  }

  [HttpDelete("{albumId}/track/{trackId}")]
  public async Task<ActionResult> RemoveTrackFromAlbum(int albumId, int trackId)
  {
    await _albumTrackService.RemoveTrackFromAlbum(albumId, trackId);
    return Ok($"Track with ID '{trackId}' was removed from album with ID '{albumId}'");
  }

  [HttpGet("{albumId}/tracks")]
  public async Task<ActionResult<IEnumerable<GetTrackDTO>>> GetAlbumTracks(int albumId)
  {
    var tracks = await _albumTrackService.GetAlbumTracks(albumId);
    var returned = tracks.Select(GetTrackDTO.FromTrack);
    return Ok(returned);
    
  }

  [HttpGet("{albumId}/artists")]
  public async Task<ActionResult<IEnumerable<GetArtistDTO>>> GetAlbumArtists(int albumId)
  {
    var artists = await _albumArtistService.FindAlbumArtists(albumId);
    var returned = artists.Select(GetArtistDTO.FromArtist);
    return Ok(returned);
  }
}