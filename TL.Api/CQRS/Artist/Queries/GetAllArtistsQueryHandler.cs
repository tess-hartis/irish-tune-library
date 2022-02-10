using MediatR;
using Microsoft.EntityFrameworkCore;
using TL.Api.DTOs.ArtistDTOs;
using TL.Repository;

namespace TL.Api.CQRS.Artist.Queries;

public record GetAllArtistsQuery : IRequest<IEnumerable<GetArtistDTO>>;

public class GetAllArtistsQueryHandler : IRequestHandler<GetAllArtistsQuery, IEnumerable<GetArtistDTO>>
{ 
    private readonly IArtistRepository _artistRepository;

    public GetAllArtistsQueryHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<IEnumerable<GetArtistDTO>> Handle
        (GetAllArtistsQuery request, CancellationToken cancellationToken)
    {
        var artists = await _artistRepository.GetEntities().ToListAsync();
        return artists.Select(GetArtistDTO.FromArtist);
        
    }

}