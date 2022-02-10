using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public class GetTuneRecordingsQuery : IRequest<IEnumerable<GetTrackDTO>>
{
    public int TuneId { get; }

    public GetTuneRecordingsQuery(int tuneId)
    {
        TuneId = tuneId;
    }
}
public class GetTuneRecordingsQueryHandler : IRequestHandler<GetTuneRecordingsQuery, IEnumerable<GetTrackDTO>>
{
    private readonly ITuneTrackService _tuneTrackService;

    public GetTuneRecordingsQueryHandler(ITuneTrackService tuneTrackService)
    {
        _tuneTrackService = tuneTrackService;
    }

    public async Task<IEnumerable<GetTrackDTO>> Handle
        (GetTuneRecordingsQuery request, CancellationToken cancellationToken)
    {
        var tracks = await _tuneTrackService.FindTracksByTune(request.TuneId);
        return tracks.Select(GetTrackDTO.FromTrack);
    }
}