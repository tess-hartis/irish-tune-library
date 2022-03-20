using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Domain;
using TL.Domain.ValueObjects.TrackTuneValueObjects;
using TL.Repository;
using static LanguageExt.Prelude;

namespace TL.CQRS.TrackCQ.Commands;

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
    private readonly ITrackTuneUnitOfWork _unitOfWork;

    public AddTrackTuneCommandHandler(ITrackTuneUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Option<Validation<Error, bool>>> Handle(AddTrackTuneCommand command,
        CancellationToken cancellationToken)
    {
        var track = await _unitOfWork.TrackRepo.FindAsync(command.TrackId);
        var tune = await _unitOfWork.TuneRepo.FindAsync(command.TuneId);
        var order = Some(TrackTuneOrder.Create(command.Order));

        var result =
            from tr in track
            from tu in tune
            from o in order
            select o.Map(TrackTune.Create)
                .Map(y => tr.AddTrackTune(y, tu));

        ignore(result.MapT(async y => await _unitOfWork.Save()));
        return result;
    }
}
