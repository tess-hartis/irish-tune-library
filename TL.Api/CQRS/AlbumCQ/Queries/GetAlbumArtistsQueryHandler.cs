using LanguageExt;
using MediatR;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Queries;

public class GetAlbumArtistsQuery : IRequest<Option<IEnumerable<Artist>>>
{
    public int AlbumId { get; }

    public GetAlbumArtistsQuery(int albumId)
    {
        AlbumId = albumId;
    }
}

public class GetAlbumArtistsQueryHandler : IRequestHandler<GetAlbumArtistsQuery, Option<IEnumerable<Artist>>>
{
    private readonly IAlbumRepository _albumRepository;

    public GetAlbumArtistsQueryHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Option<IEnumerable<Artist>>> Handle
        (GetAlbumArtistsQuery request, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.FindAsync(request.AlbumId);
        var artists = album.Select(a => a.Artists);
        return artists;
    }
}