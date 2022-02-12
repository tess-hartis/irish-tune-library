using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Queries;

public class GetAlbumTracksQuery : IRequest<IEnumerable<Track>>
{
    public int AlbumId { get; }

    public GetAlbumTracksQuery(int albumId)
    {
        AlbumId = albumId;
    }
}
public class GetAlbumTracksQueryHandler : IRequestHandler<GetAlbumTracksQuery, IEnumerable<Track>>
{
    private readonly IAlbumTrackService _albumTrackService;

    public GetAlbumTracksQueryHandler(IAlbumTrackService albumTrackService)
    {
        _albumTrackService = albumTrackService;
    }

    public async Task<IEnumerable<Track>> Handle
        (GetAlbumTracksQuery request, CancellationToken cancellationToken)
    {
        var tracks = await _albumTrackService.GetAlbumTracks(request.AlbumId);
        return tracks;

    }
}