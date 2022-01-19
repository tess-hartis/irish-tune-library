using Microsoft.AspNetCore.Mvc;
using TL.Api.AlbumDTOs;
using TL.Api.TrackDTOs;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/Albums")]
public class AlbumController : Controller
{
  private readonly IAlbumRepository _albumRepository;
  private readonly IUnitOfWork _unitOfWork;

  public AlbumController(IAlbumRepository albumRepository, IUnitOfWork unitOfWork)
  {
    _albumRepository = albumRepository;
    _unitOfWork = unitOfWork;
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
    var albums = await _albumRepository.GetByTitle(title);
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

  [HttpGet("{trackId}")]
  public async Task<ActionResult<GetAlbumDTO>> GetAlbumByTrack(int trackId)
  {
    var album = await _unitOfWork.GetAlbumByTrack(trackId);
    var returned = GetAlbumDTO.FromAlbum(album);
    return Ok(returned);
  }
  
  [HttpPost]
  public async Task<ActionResult> AddAlbum([FromBody] PostAlbumDTO dto)
  {
    var album = PostAlbumDTO.ToAlbum(dto);
    await _albumRepository.Add(album);
    return Ok();
  }
  
  [HttpPut("{id}")]
  public async Task<ActionResult> PutAlbum(int id, [FromBody] PutAlbumDTO dto)
  {
    var album = await _albumRepository.FindAsync(id);
    var updated = PutAlbumDTO.UpdatedAlbum(album, dto);
    await _albumRepository.Update(album.Id);
    return Ok();
  }
  
  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteAlbum(int id)
  {
    var album = _albumRepository.FindAsync(id);
    await _albumRepository.Delete(album.Id);
    return Ok();

  }
}