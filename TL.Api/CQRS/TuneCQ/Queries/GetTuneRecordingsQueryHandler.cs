using LanguageExt;
using LanguageExt.SomeHelp;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
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


        // var tracks = tune.Map(async t => await _tuneTrackService.FindTracksByTune(t));

        // var trizzles =
        //     from t in tune // Option<Tune>  => Tune
        //     // from trizz in t.FeaturedOnTrack // List<TrackTune>  => TrackTune
        //     select t.FeaturedOnTrack.Select(x => x.Track); // TrackTune => Track
        //
        // var tracks =
        //     tune
        //         .Map(t => t.FeaturedOnTrack)
        //         .Map(x => x.Map(x => x.Track));
        //
        // var trackEquivalent =
        //     tune
        //         .Select(t =>
        //             t.FeaturedOnTrack.Select(t
        //                 => t.Track));
        //
        // var hmmmTracks =
        //     tune
        //         .SelectMany(x => x.FeaturedOnTrack)
        //         .Select(x => x.Track);

        // [ [1,2], [2,3,3]]
        // SelectMany: List<List<int>> => List<int>

        // SelectMany: Option<List<TrackTune>>  => List<TrackTune>
        // SelectMany: List<TrackTune>  => TrackTune
        // Return List<TrackTune>  => Option<List<TrackTune>>


        // return trizzles;
    }
}