using LanguageExt;
using MediatR;
using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.ArtistCQ.Queries;

public class GetArtistAlbumsQuery : IRequest<Option<IEnumerable<Album>>>
{
    public int ArtistId { get; }

    public GetArtistAlbumsQuery(int artistId)
    {
        ArtistId = artistId;
    }
}

public class GetArtistAlbumsQueryHandler : IRequestHandler<GetArtistAlbumsQuery, Option<IEnumerable<Album>>>
{
    private readonly IArtistRepository _artistRepository;

    public GetArtistAlbumsQueryHandler(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Option<IEnumerable<Album>>> Handle
        (GetArtistAlbumsQuery request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.FindAsync(request.ArtistId);
        var albums = artist.Select(a => a.Albums);
        return albums;

    }
}