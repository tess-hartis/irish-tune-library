using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.AlbumDTOs;
using TL.Api.ArtistDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]
public class ArtistController : Controller
{
   private readonly IArtistRepository _artistRepository;
   private readonly IAlbumArtistService _albumArtistService;

   public ArtistController(IArtistRepository artistRepository, IAlbumArtistService albumArtistService)
   {
      _artistRepository = artistRepository;
      _albumArtistService = albumArtistService;
   }
   
   [HttpGet]
   public async Task<ActionResult<IEnumerable<GetArtistDTO>>> FindAll()
   {
      var artists = await _artistRepository.GetEntities().ToListAsync();
      var returned = artists.Select(GetArtistDTO.FromArtist);
      return Ok(returned);
   }

   [HttpGet("{id}")]
   public async Task<ActionResult<GetArtistDTO>> FindArtist(int id)
   {
      var artist = await _artistRepository.FindAsync(id);
      var returned = GetArtistDTO.FromArtist(artist);
      return Ok(returned);
   }

   [HttpPost]
   public async Task<ActionResult> AddArtist([FromBody] PostArtistDTO dto)
   {
      var artist = PostArtistDTO.ToArtist(dto);
      await _artistRepository.AddAsync(artist);
      var returned = GetArtistDTO.FromArtist(artist);
      return CreatedAtAction(nameof(FindArtist), new {id = artist.Id}, returned);
   }

   [HttpPut("{id}")]
   public async Task<ActionResult> PutArtist(int id, [FromBody] PutArtistDTO dto)
   {
      var name = ArtistName.Create(dto.Name);
      await _artistRepository.UpdateArtist(id, name);
      return Ok($"Artist with ID '{id}' was updated");
   }
   
   [HttpDelete("{id}")]
   public async Task<ActionResult> DeleteArtist(int id)
   {
      await _artistRepository.DeleteAsync(id);
      return Ok($"Artist with ID '{id}' was deleted");

   }

   [HttpGet("{artistId}/albums")]
   public async Task<ActionResult<IEnumerable<GetAlbumDTO>>> FindArtistAlbums(int artistId)
   {
      var albums = await _albumArtistService.FindArtistAlbums(artistId);
      var returned = albums.Select(GetAlbumDTO.FromAlbum);
      return Ok(returned);
   }
   
}