using MediatR;
using TL.Repository;

namespace TL.Api.CQRS.Track.Commands;

public class DeleteTrackCommand : IRequest<Unit>
{
    public int Id { get; }

    public DeleteTrackCommand(int id)
    {
        Id = id;
    }
}
public class DeleteTrackCommandHandler : IRequestHandler<DeleteTrackCommand, Unit>
{
    private readonly ITrackRepository _trackRepository;

    public DeleteTrackCommandHandler(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<Unit> Handle(DeleteTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await _trackRepository.FindAsync(command.Id);
        await _trackRepository.DeleteAsync(track.Id);
        return Unit.Value;
    }
}