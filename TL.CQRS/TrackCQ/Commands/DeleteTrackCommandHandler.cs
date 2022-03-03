using LanguageExt;
using MediatR;
using TL.Repository;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace TL.CQRS.TrackCQ.Commands;

public class DeleteTrackCommand : IRequest<Option<Unit>>
{
    public int Id { get; }

    public DeleteTrackCommand(int id)
    {
        Id = id;
    }
}
public class DeleteTrackCommandHandler : IRequestHandler<DeleteTrackCommand, Option<Unit>>
{
    private readonly ITrackRepository _trackRepository;

    public DeleteTrackCommandHandler(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<Option<Unit>> Handle(DeleteTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await _trackRepository.FindAsync(command.Id);
        ignore(track.Map(async t => await _trackRepository.DeleteAsync(t)));
        return track.Map(t => unit);
    }
}