using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TL.Api.CQRS.AlbumCQ.Commands;
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
      var artist = await _mediator.Send(query);
      return artist
         .Map(GetArtistDTO.FromArtist)
         .Some<IActionResult>(Ok)
         .None(NotFound);

   }

   [HttpPost]
   public async Task<IActionResult> AddArtist([FromBody] CreateArtistCommand request)
   {
      var artist = await _mediator.Send(request);
      return artist.Match<IActionResult>(
         a => Ok(GetArtistDTO.FromArtist(a)),
         e =>
         {
            var errorList = e.Select(e => e.Message).ToList();
            return UnprocessableEntity(new {code = 422, errors = errorList});
         });
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> PutArtist(int id, [FromBody] UpdateArtistCommand request)
   {
      request.Id = id;
      var artist = await _mediator.Send(request);
      return artist.Match<IActionResult>(
         a => Ok(GetArtistDTO.FromArtist(a)),
         e =>
         {
            var errorList = e.Select(e => e.Message).ToList();
            return UnprocessableEntity(new {code = 422, errors = errorList});
         });
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