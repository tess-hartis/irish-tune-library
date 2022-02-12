using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public class GetTuneRecordingsQuery : IRequest<IEnumerable<Track>>
{
    public int TuneId { get; }

    public GetTuneRecordingsQuery(int tuneId)
    {
        TuneId = tuneId;
    }
}
public class GetTuneRecordingsQueryHandler : IRequestHandler<GetTuneRecordingsQuery, IEnumerable<Track>>
{
    private readonly ITuneTrackService _tuneTrackService;

    public GetTuneRecordingsQueryHandler(ITuneTrackService tuneTrackService)
    {
        _tuneTrackService = tuneTrackService;
    }

    public async Task<IEnumerable<Track>> Handle
        (GetTuneRecordingsQuery request, CancellationToken cancellationToken)
    {
        var tracks = await _tuneTrackService.FindTracksByTune(request.TuneId);
        return tracks;
    }
}