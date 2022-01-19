using Microsoft.AspNetCore.Mvc;
using TL.Api.ArtistDTOs;
using TL.Repository;

namespace TL.Api.Controllers;

public class ArtistController : Controller
{
   private readonly IArtistRepository _artistRepository;
   private readonly IUnitOfWork _unitOfWork;

   public ArtistController(IArtistRepository artistRepository, IUnitOfWork unitOfWork)
   {
      _artistRepository = artistRepository;
      _unitOfWork = unitOfWork;
   }
   
   [HttpGet]
   public async Task<ActionResult<IEnumerable<GetArtistsDTO>>> FindAll()
   {
      var artists = await _artistRepository.GetAllArtists();
      var returned = GetArtistsDTO.GetAll(artists);
      return Ok(returned);
   }
   
   [HttpGet("{name}")]
   public async Task<ActionResult<IEnumerable<GetArtistsDTO>>> FindByName(string name)
   {
      var artists = await _artistRepository.GetByName(name);
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

   [HttpGet("{albumId}")]
   public async Task<ActionResult<GetArtistsDTO>> GetAlbumArtists(int albumId)
   {
      var artist = await _unitOfWork.GetAlbumArtists(albumId);
      var returned = GetArtistsDTO.GetAll(artist);
      return Ok(returned);
   }

   [HttpPost]
   public async Task<ActionResult> AddArtist([FromBody] PostArtistDTO dto)
   {
      var artist = PostArtistDTO.ToArtist(dto);
      await _artistRepository.Add(artist);
      return Ok();
   }

   [HttpPut("{id}")]
   public async Task<ActionResult> PutArtist(int id, [FromBody] PutArtistDTO dto)
   {
      var artist = await _artistRepository.FindAsync(id);
      var updated = PutArtistDTO.UpdatedArtist(artist, dto);
      await _artistRepository.Update(artist.Id);
      return Ok();
   }
   
   [HttpDelete("{id}")]
   public async Task<ActionResult> DeleteArtist(int id)
   {
      var artist = _artistRepository.FindAsync(id);
      await _artistRepository.Delete(artist.Id);
      return Ok();

   }
}