using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class AddAlternateTitleCommand : IRequest<GetTuneDTO>
{
    public int Id { get; }
    public TuneTitle AlternateTitle { get; }

    public AddAlternateTitleCommand(int id, TuneTitle alternateTitle)
    {
        Id = id;
        AlternateTitle = alternateTitle;
    }
}
public class AddAlternateTitleCommandHandler : IRequestHandler<AddAlternateTitleCommand, GetTuneDTO>
{
    private readonly ITuneRepository _tuneRepository;

    public AddAlternateTitleCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<GetTuneDTO> Handle(AddAlternateTitleCommand command, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(command.Id);
        await _tuneRepository.AddAlternateTitle(tune.Id, command.AlternateTitle);
        return GetTuneDTO.FromTune(tune);
    }
}