using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.CQRS.ArtistCQ.Commands;
using TL.Api.CQRS.ArtistCQ.Queries;
using TL.Api.DTOs.AlbumDTOs;
using TL.Api.DTOs.ArtistDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Repository;

namespace TL.Api.Controllers;

[Route("api/[controller]")]
public class ArtistController : Controller
{
   private readonly IMediator _mediator;

   public ArtistController(IMediator mediator)
   {
      _mediator = mediator;
   }
   
   [HttpGet]
   public async Task<IActionResult> FindAll()
   {
      var query = new GetAllArtistsQuery();
      var result = await _mediator.Send(query);
      return Ok(result.Select(GetArtistDTO.FromArtist));
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> FindArtist(int id)
   {
      var query = new GetArtistByIdQuery(id);
      var result = await _mediator.Send(query);
      return Ok(GetArtistDTO.FromArtist(result));

   }

   [HttpPost]
   public async Task<IActionResult> AddArtist([FromBody] PostArtistDTO dto)
   {
      var artist = PostArtistDTO.ToArtist(dto);
      var command = new CreateArtistCommand(artist.Name);
      var result = await _mediator.Send(command);
      return Ok(GetArtistDTO.FromArtist(result));
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> PutArtist(int id, [FromBody] PutArtistDTO dto)
   {
      var name = ArtistName.Create(dto.Name);
      var command = new UpdateArtistCommand(id, name);
      var result = await _mediator.Send(command);
      return Ok(GetArtistDTO.FromArtist(result));
   }
   
   [HttpDelete("{id}")]
   public async Task<IActionResult> DeleteArtist(int id)
   {
      var command = new DeleteArtistCommand(id);
      await _mediator.Send(command);
      return Ok($"Artist with ID '{id}' was deleted");

   }

   [HttpGet("{artistId}/albums")]
   public async Task<IActionResult> FindArtistAlbums(int artistId)
   {
      var query = new GetArtistAlbumsQuery(artistId);
      var result = await _mediator.Send(query);
      return Ok(result.Select(GetAlbumDTO.FromAlbum));
   }
   
}