using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.TrackTuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Commands;

public class AddTrackTuneCommand : IRequest<Validation<Error, TrackTune>>
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
public class AddTrackTuneCommandHandler : IRequestHandler<AddTrackTuneCommand, Validation<Error, TrackTune>>
{
    private readonly ITuneTrackService _tuneTrackService;

    public AddTrackTuneCommandHandler(ITuneTrackService tuneTrackService)
    {
        _tuneTrackService = tuneTrackService;
    }

    public async Task<Validation<Error, TrackTune>> Handle(AddTrackTuneCommand command, CancellationToken cancellationToken)
    {
        var order = TrackTuneOrder.Create(command.Order);
        var trackTune = order
            .Map(o => TrackTune.Create(command.TrackId, command.TuneId, o));

        await trackTune
            .Succ(async t =>
            {
                await _tuneTrackService.AddExistingTuneToTrack(command.TrackId, command.TuneId, t);
            })
            .Fail(e => e.AsTask());

        return trackTune;
    }
}