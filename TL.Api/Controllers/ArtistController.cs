using MediatR;
using Microsoft.AspNetCore.Mvc;
using TL.Api.DTOs.AlbumDTOs;
using TL.Api.DTOs.ArtistDTOs;
using TL.CQRS.ArtistCQ.Commands;
using TL.CQRS.ArtistCQ.Queries;

namespace TL.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

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
         a => Ok(),
         e =>
         {
            var errors = e.Select(e => e.Message).ToList();
            return UnprocessableEntity(new {errors});
         });
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> PutArtist(int id, [FromBody] UpdateArtistCommand request)
   {
      request.Id = id;
      var artist = await _mediator.Send(request);
      return artist
         .Some(x =>
            x.Succ<IActionResult>(u => Ok())
               .Fail(e =>
               {
                  var errors = e.Select(x => x.Message).ToList();
                  return UnprocessableEntity(new {errors});
               }))
         .None(NotFound);
   }
   
   [HttpDelete("{id}")]
   public async Task<IActionResult> DeleteArtist(int id)
   {
      var command = new DeleteArtistCommand(id);
      var result = await _mediator.Send(command);
      return result
         .Some<IActionResult>(_ => NoContent())
         .None(NotFound);
   }

   [HttpGet("{artistId}/albums")]
   public async Task<IActionResult> FindArtistAlbums(int artistId)
   {
      var query = new GetArtistAlbumsQuery(artistId);
      var albums = await _mediator.Send(query);
      return albums
         .Some<IActionResult>(a =>
            Ok(a.Select(GetAlbumDTO.FromAlbum)))
         .None(NotFound);
   }
}