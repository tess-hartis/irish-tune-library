using Microsoft.AspNetCore.Mvc;
using TL.Api.AlbumDTOs;
using TL.Api.ArtistDTOs;
using TL.Domain;
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
   public async Task<ActionResult<IEnumerable<GetArtistsDTO>>> FindAll()
   {
      var artists = await _artistRepository.GetAllArtists();
      var returned = GetArtistsDTO.GetAll(artists);
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
      await _artistRepository.AddArtist(artist);
      return Ok();
   }

   [HttpPut("{id}")]
   public async Task<ActionResult> PutArtist(int id, [FromBody] PutArtistDTO dto)
   {
      var artist = await _artistRepository.FindAsync(id);
      var updated = PutArtistDTO.UpdatedArtist(artist, dto);
      await _artistRepository.UpdateArtist(updated.Id, dto.Name);
      return Ok();
   }
   
   [HttpDelete("{id}")]
   public async Task<ActionResult> DeleteArtist(int id)
   {
      var artist = _artistRepository.FindAsync(id);
      await _artistRepository.DeleteArtist(artist.Id);
      return Ok();

   }

   [HttpGet("{artistId}/albums")]
   public async Task<ActionResult<IEnumerable<GetAlbumsDTO>>> FindArtistAlbums(int artistId)
   {
      var albums = await _albumArtistService.GetAlbumsByArtist(artistId);
      var returned = GetAlbumsDTO.GetAll(albums);
      return Ok(returned);
   }
   
}