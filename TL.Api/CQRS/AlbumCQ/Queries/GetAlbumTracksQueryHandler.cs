using LanguageExt;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Queries;

public class GetAlbumTracksQuery : IRequest<Option<IEnumerable<Track>>>
{
    public int AlbumId { get; }

    public GetAlbumTracksQuery(int albumId)
    {
        AlbumId = albumId;
    }
}
public class GetAlbumTracksQueryHandler : IRequestHandler<GetAlbumTracksQuery, Option<IEnumerable<Track>>>
{
    private readonly IAlbumRepository _albumRepository;

    public GetAlbumTracksQueryHandler(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Option<IEnumerable<Track>>> Handle
        (GetAlbumTracksQuery request, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.FindAsync(request.AlbumId);
        var tracks = album.Select(a => a.TrackListing);
        return tracks;

    }
}