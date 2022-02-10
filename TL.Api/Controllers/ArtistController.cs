using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.CQRS.Artist.Queries;
using TL.Api.DTOs.AlbumDTOs;
using TL.Api.DTOs.ArtistDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]
public class ArtistController : Controller
{
   private readonly IArtistRepository _artistRepository;
   private readonly IAlbumArtistService _albumArtistService;
   private readonly IMediator _mediator;

   public ArtistController(IArtistRepository artistRepository, IAlbumArtistService albumArtistService, IMediator mediator)
   {
      _artistRepository = artistRepository;
      _albumArtistService = albumArtistService;
      _mediator = mediator;
   }
   
   [HttpGet]
   public async Task<ActionResult<IEnumerable<GetArtistDTO>>> FindAll()
   {
      var query = new GetAllArtistsQuery();
      var result = await _mediator.Send(query);
      return Ok(result);
   }

   [HttpGet("{id}")]
   public async Task<ActionResult<GetArtistDTO>> FindArtist(int id)
   {
      var query = new GetArtistByIdQuery(id);
      var result = await _mediator.Send(query);
      return Ok(result);

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
      var query = new GetArtistAlbumsQuery(artistId);
      var result = await _mediator.Send(query);
      return Ok(result);
   }
   
}