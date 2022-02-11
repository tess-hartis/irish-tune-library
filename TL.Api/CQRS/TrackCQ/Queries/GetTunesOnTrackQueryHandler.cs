using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Queries;

public class GetTunesOnTrackQuery : IRequest<IEnumerable<GetTrackTuneDTO>>
{
    public int TrackId { get; }
    
    public GetTunesOnTrackQuery(int trackId)
    {
        TrackId = trackId;
    }
}
public class GetTunesOnTrackQueryHandler : IRequestHandler<GetTunesOnTrackQuery, IEnumerable<GetTrackTuneDTO>>
{
    private readonly ITuneTrackService _tuneTrackService;

    public GetTunesOnTrackQueryHandler(ITuneTrackService tuneTrackService)
    {
        _tuneTrackService = tuneTrackService;
    }

    public async Task<IEnumerable<GetTrackTuneDTO>> Handle
        (GetTunesOnTrackQuery request, CancellationToken cancellationToken)
    {
        var tunesOnTrack = await _tuneTrackService.GetTrackTunes(request.TrackId);
        return tunesOnTrack.Select(GetTrackTuneDTO.FromTrackTune);
    }
}