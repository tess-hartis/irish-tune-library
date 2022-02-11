using MediatR;
using TL.Domain;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class DeleteTuneCommand : IRequest<Unit>
{
    public int Id;

    public DeleteTuneCommand(int id)
    {
        Id = id;
    }
}
public class DeleteTuneCommandHandler : IRequestHandler<DeleteTuneCommand, Unit>
{
    private readonly ITuneRepository _tuneRepository;

    public DeleteTuneCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Unit> Handle(DeleteTuneCommand command, CancellationToken cancellationToken)
    {
        await _tuneRepository.DeleteAsync(command.Id);
        return Unit.Value;
    }
}