using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Repository;

namespace TL.Api.CQRS.Track.Commands;

public class RemoveTrackTuneCommand : IRequest<Unit>
{
    public int TrackId { get; }
    public int TuneId { get; }

    public RemoveTrackTuneCommand(int trackId, int tuneId)
    {
        TrackId = trackId;
        TuneId = tuneId;
    }
}
public class RemoveTrackTuneCommandHandler : IRequestHandler<RemoveTrackTuneCommand, Unit>
{
    private readonly ITuneTrackService _tuneTrackService;

    public RemoveTrackTuneCommandHandler(ITuneTrackService tuneTrackService)
    {
        _tuneTrackService = tuneTrackService;
    }

    public async Task<Unit> Handle(RemoveTrackTuneCommand command, CancellationToken cancellationToken)
    {
        await _tuneTrackService.RemoveTuneFromTrack(command.TrackId, command.TuneId);
        return Unit.Value;
    }
}