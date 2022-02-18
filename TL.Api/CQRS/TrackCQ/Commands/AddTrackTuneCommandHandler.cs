using LanguageExt;
using LanguageExt.Common;
using LanguageExt.SomeHelp;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.TrackTuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Commands;

public class AddTrackTuneCommand : IRequest<Option<Validation<Error, TrackTune>>>
{
    public int TrackId { get; set; }
    public int TuneId { get; set; }
    public int Order { get; }

    public AddTrackTuneCommand(int trackId, int tuneId, int order)
    {
        TrackId = trackId;
        TuneId = tuneId;
        Order = order;
    }
}
public class AddTrackTuneCommandHandler : IRequestHandler<AddTrackTuneCommand, Option<Validation<Error, TrackTune>>>
{
    private readonly ITrackRepository _trackRepository;
    private readonly ITuneRepository _tuneRepository;
    private readonly ITrackTuneRepository _trackTuneRepository;

    public AddTrackTuneCommandHandler(ITrackRepository trackRepository, ITuneRepository tuneRepository, ITrackTuneRepository trackTuneRepository)
    {
        _trackRepository = trackRepository;
        _tuneRepository = tuneRepository;
        _trackTuneRepository = trackTuneRepository;
    }

    public async Task<Option<Validation<Error, TrackTune>>> Handle(AddTrackTuneCommand command,
        CancellationToken cancellationToken)
    {
        var track = await _trackRepository.FindAsync(command.TrackId);
        var tune = await _tuneRepository.FindAsync(command.TuneId);
        var order = Some(TrackTuneOrder.Create(command.Order));

        var result =
            from tr in track
            from tu in tune
            from o in order
            select o.Map(x => TrackTune.Create(tr, tu, x));

        ignore(result.MapT(async y => await _trackTuneRepository.AddAsync(y)));

        return result;

        // var succTrack = Success<Error, Option<Track>>(track);
        // var succTune = Success<Error, Option<Tune>>(tune);

        // var newTrackTune = (track, tune).Apply((tr, tu) => TrackTune.Create(tr, tu, or));
        //
        // var newtttt = track
        //     .Bind(x => tune.Map(y => order
        //         .Map(o => TrackTune.Create(x, y, o))));
        //
        // ignore(newtttt.Map(t => t
        //     .Map(async x => await _trackTuneRepository.AddAsync(x))));
        //
        // return newtttt;

        // var querySyntax = from tr in track from tu in tune from o in order select TrackTune.Create(tr, tu, o);
        //
        // var sdjfs = from or in order select or.Value;
        //
        // var optOne = 1.ToSome().ToOption();
        //
        // var result = optOne.Bind(x => x > 5 ? Some(x + 1) : Option<int>.None);


    }
}


        // M(a -> b) -> Ma -> Mb