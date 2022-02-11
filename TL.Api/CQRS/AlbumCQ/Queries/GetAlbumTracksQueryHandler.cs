using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Queries;

public class GetAlbumTracksQuery : IRequest<IEnumerable<GetTrackDTO>>
{
    public int AlbumId { get; }

    public GetAlbumTracksQuery(int albumId)
    {
        AlbumId = albumId;
    }
}
public class GetAlbumTracksQueryHandler
{
    private readonly IAlbumTrackService _albumTrackService;

    public GetAlbumTracksQueryHandler(IAlbumTrackService albumTrackService)
    {
        _albumTrackService = albumTrackService;
    }

    public async Task<IEnumerable<GetTrackDTO>> Handle
        (GetAlbumTracksQuery request, CancellationToken cancellationToken)
    {
        var tracks = await _albumTrackService.GetAlbumTracks(request.AlbumId);
        return tracks.Select(GetTrackDTO.FromTrack);
        
    }
}