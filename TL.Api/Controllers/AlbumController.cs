using Microsoft.AspNetCore.Mvc;
using TL.Api.AlbumDTOs;
using TL.Api.TrackDTOs;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]
public class AlbumController : Controller
{
  private readonly IAlbumRepository _albumRepository;
  private readonly IAlbumArtistService _albumArtistService;

  public AlbumController(IAlbumRepository albumRepository, IAlbumArtistService albumArtistService)
  {
    _albumRepository = albumRepository;
    _albumArtistService = albumArtistService;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<GetAlbumDTO>> FindAlbum(int id)
  {
    var album = await _albumRepository.FindAsync(id);
    var returned = GetAlbumDTO.FromAlbum(album);
    return Ok(returned);
  }

  [HttpGet("{title}")]
  public async Task<ActionResult<IEnumerable<GetTracksDTO>>> FindByTitle(string title)
  {
    var albums = await _albumRepository.FindByExactTitle(title);
    var returned = GetAlbumsDTO.GetAll(albums);
    return Ok(returned);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<GetAlbumsDTO>>> FindAll()
  {
    var albums = await _albumRepository.GetAllAlbums();
    var returned = GetAlbumsDTO.GetAll(albums);
    return Ok(returned);
  }
  
  
  [HttpPost]
  public async Task<ActionResult> AddAlbum([FromBody] PostAlbumDTO dto)
  {
    var album = PostAlbumDTO.ToAlbum(dto);
    await _albumRepository.AddAlbum(album);
    return Ok();
  }
  
  [HttpPut("{id}")]
  public async Task<ActionResult> PutAlbum(int id, [FromBody] PutAlbumDTO dto)
  {
    var album = await _albumRepository.FindAsync(id);
    var updated = PutAlbumDTO.UpdatedAlbum(album, dto);
    await _albumRepository.UpdateAsync(updated);
    return Ok();
  }
  
  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteAlbum(int id)
  {
    var album = _albumRepository.FindAsync(id);
    await _albumRepository.DeleteAlbum(album.Id);
    return Ok();

  }
}