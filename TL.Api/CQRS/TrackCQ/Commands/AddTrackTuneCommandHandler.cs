using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.TrackTuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Commands;

public class AddTrackTuneCommand : IRequest<Track>
{
    public int TrackId { get; }
    public int TuneId { get; }
    public TrackTuneOrder Order { get; }

    public AddTrackTuneCommand(int trackId, int tuneId, TrackTuneOrder order)
    {
        TrackId = trackId;
        TuneId = tuneId;
        Order = order;
    }
}
public class AddTrackTuneCommandHandler : IRequestHandler<AddTrackTuneCommand, Track>
{
    private readonly ITuneTrackService _tuneTrackService;

    public AddTrackTuneCommandHandler(ITuneTrackService tuneTrackService)
    {
        _tuneTrackService = tuneTrackService;
    }

    public async Task<Track> Handle(AddTrackTuneCommand command, CancellationToken cancellationToken)
    {
        var track = await _tuneTrackService.AddExistingTuneToTrack(command.TrackId, command.TuneId, command.Order);
        return track;
    }
}