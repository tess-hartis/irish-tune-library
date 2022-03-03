using MediatR;
using TL.Domain;
using TL.Repository;

namespace TL.CQRS.ArtistCQ.Queries;

public record GetAllArtistsQuery : IRequest<IEnumerable<Artist>>;

public class GetAllArtistsQueryHandler : IRequestHandler<GetAllArtistsQuery, IEnumerable<Artist>>
{ 
    private readonly IArtistRepository _artistRepository;

    public GetAllArtistsQueryHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<IEnumerable<Artist>> Handle
        (GetAllArtistsQuery request, CancellationToken cancellationToken)
    {
        var artists = await _artistRepository.GetAllArtists();
        return artists;

    }

}