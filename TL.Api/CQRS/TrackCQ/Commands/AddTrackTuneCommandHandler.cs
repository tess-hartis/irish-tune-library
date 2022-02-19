using LanguageExt;
using LanguageExt.Common;
using LanguageExt.SomeHelp;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Data;
using TL.Domain;
using TL.Domain.ValueObjects.TrackTuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Commands;

public class AddTrackTuneCommand : IRequest<Option<Validation<Error, bool>>>
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
public class AddTrackTuneCommandHandler : IRequestHandler<AddTrackTuneCommand, Option<Validation<Error, bool>>>
{
    private readonly TuneLibraryContext _context;
    private ITrackRepository _trackRepository;
    private ITuneRepository _tuneRepository;
    private ITrackTuneRepository _trackTuneRepository;

    public AddTrackTuneCommandHandler(TuneLibraryContext context)
    {
        _context = context;
    }

    private ITrackRepository TrackRepo
    {
        get
        {
            return _trackRepository = new TrackRepository(_context);
        }
    }

    private ITuneRepository TuneRepo
    {
        get
        {
            return _tuneRepository = new TuneRepository(_context);
        }
    }

    private ITrackTuneRepository TrackTuneRepo
    {
        get
        {
            return _trackTuneRepository = new TrackTuneRepository(_context);
        }
    }

    public async Task<Option<Validation<Error, bool>>> Handle(AddTrackTuneCommand command,
        CancellationToken cancellationToken)
    {
        var track = await TrackRepo.FindAsync(command.TrackId);
        var tune = await TuneRepo.FindAsync(command.TuneId);
        var order = Some(TrackTuneOrder.Create(command.Order));

        var result =
            from tr in track
            from tu in tune
            from o in order
            select o.Map(TrackTune.Create)
                .Map(y => tr.AddTrackTune(y, tu));

        ignore(result.MapT(async y => await _context.SaveChangesAsync()));

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