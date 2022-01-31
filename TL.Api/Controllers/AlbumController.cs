using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.AlbumDTOs;
using TL.Api.ArtistDTOs;
using TL.Api.TrackDTOs;
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
  public async Task<ActionResult<IEnumerable<GetAlbumsDTO>>> FindAllAlbums()
  {
    var albums = await _albumRepository.GetEntities().ToListAsync();
    var returned = GetAlbumsDTO.GetAll(albums);
    return Ok(returned);
  }
  
  [HttpPost]
  public async Task<ActionResult> AddAlbum([FromBody] PostAlbumDTO dto)
  {
    var album = PostAlbumDTO.ToAlbum(dto);
    await _albumRepository.AddAsync(album);
    return Ok();
  }
  
  [HttpPut("{id}")]
  public async Task<ActionResult> PutAlbum(int id, [FromBody] PutAlbumDTO dto)
  {
    var album = await _albumRepository.FindAsync(id);
    var updated = PutAlbumDTO.UpdatedAlbum(album, dto);
    await _albumRepository.UpdateAlbum(updated, dto.Title, dto.Year);
    return Ok();
  }
  
  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteAlbum(int id)
  {
    var isDeleted = await _albumRepository.DeleteAsync(id);
    if (!isDeleted)
    {
      return NotFound($"Album with ID '{id}' was not found");
    }
    
    return Ok();
  }

  [HttpPost("{albumId}/artist/{artistId}")]
  public async Task<ActionResult> AddExistingArtistToAlbum(int albumId, int artistId)
  {
    await _albumArtistService.AddExistingArtistToAlbum(albumId, artistId);
    return Ok();
  }

  [HttpPost("{albumId}/artist")]
  public async Task<ActionResult> AddNewArtistToAlbum(int albumId, [FromBody] PostArtistDTO dto)
  {
    await _albumArtistService.AddNewArtistToAlbum(albumId, dto.Name);
    return Ok();
  }

  [HttpDelete("{albumId}/artist/{artistId}")]
  public async Task<ActionResult> RemoveArtistFromAlbum(int albumId, int artistId)
  {
    await _albumArtistService.RemoveArtistFromAlbum(albumId, artistId);
    return Ok();
  }

  [HttpPost("{albumId}/track")]
  public async Task<ActionResult> AddTrackToAlbum(int albumId, [FromBody] PostTrackDTO dto)
  {
    await _albumTrackService.AddNewTrackToAlbum(albumId, dto.Title, dto.TrackNumber);
    return Ok();
  }

  [HttpDelete("{albumId}/track/{trackId}")]
  public async Task<ActionResult> RemoveTrackFromAlbum(int albumId, int trackId)
  {
    await _albumTrackService.RemoveTrackFromAlbum(albumId, trackId);
    return Ok();
  }
}