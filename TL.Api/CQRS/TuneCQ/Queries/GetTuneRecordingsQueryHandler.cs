using LanguageExt;
using MediatR;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Queries;

public class GetTuneRecordingsQuery : IRequest<Option<IEnumerable<Track>>>
{
    public int TuneId { get; }

    public GetTuneRecordingsQuery(int tuneId)
    {
        TuneId = tuneId;
    }
}

public class GetTuneRecordingsQueryHandler :
    IRequestHandler<GetTuneRecordingsQuery, Option<IEnumerable<Track>>>
{
    private readonly ITuneRepository _tuneRepository;

    public GetTuneRecordingsQueryHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Option<IEnumerable<Track>>> Handle
        (GetTuneRecordingsQuery request, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(request.TuneId);
        var tracks = tune.Select(t => t.FeaturedOnTrack);
        return tracks;
    }
}