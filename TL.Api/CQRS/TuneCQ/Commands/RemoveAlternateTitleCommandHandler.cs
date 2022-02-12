using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class RemoveAlternateTitleCommand : IRequest<Tune>
{
    public int Id { get; }
    public TuneTitle AlternateTitle { get; }

    public RemoveAlternateTitleCommand(int id, TuneTitle alternateTitle)
    {
        Id = id;
        AlternateTitle = alternateTitle;
    }
}
public class RemoveAlternateTitleCommandHandler : IRequestHandler<RemoveAlternateTitleCommand, Tune>
{
    private readonly ITuneRepository _tuneRepository;

    public RemoveAlternateTitleCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Tune> Handle(RemoveAlternateTitleCommand command, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(command.Id);
        await _tuneRepository.RemoveAlternateTitle(tune.Id, command.AlternateTitle);
        return tune;
    }
}