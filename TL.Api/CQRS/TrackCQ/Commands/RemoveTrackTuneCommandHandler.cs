using MediatR;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Commands;

public class RemoveTrackTuneCommand : IRequest<Track>
{
    public int TrackId { get; }
    public int TuneId { get; }

    public RemoveTrackTuneCommand(int trackId, int tuneId)
    {
        TrackId = trackId;
        TuneId = tuneId;
    }
}
public class RemoveTrackTuneCommandHandler : IRequestHandler<RemoveTrackTuneCommand, Track>
{
    private readonly ITuneTrackService _tuneTrackService;

    public RemoveTrackTuneCommandHandler(ITuneTrackService tuneTrackService)
    {
        _tuneTrackService = tuneTrackService;
    }

    public async Task<Track> Handle(RemoveTrackTuneCommand command, CancellationToken cancellationToken)
    {
        var track = await _tuneTrackService.RemoveTuneFromTrack(command.TrackId, command.TuneId);
        return track;
        
        //update DTO to also show tunes on track
    }
}