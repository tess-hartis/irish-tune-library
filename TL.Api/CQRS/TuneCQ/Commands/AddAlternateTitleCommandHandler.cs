using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class AddAlternateTitleCommand : IRequest<Validation<Error, Tune>>
{
    public int Id { get; set; }
    public string AlternateTitle { get; }

    public AddAlternateTitleCommand(int id, string alternateTitle)
    {
        Id = id;
        AlternateTitle = alternateTitle;
    }
}
public class AddAlternateTitleCommandHandler : IRequestHandler<AddAlternateTitleCommand, Validation<Error, Tune>>
{
    private readonly ITuneRepository _tuneRepository;

    public AddAlternateTitleCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Validation<Error, Tune>> Handle(AddAlternateTitleCommand command, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(command.Id);
        var alternateTitle = TuneTitle.Create(command.AlternateTitle);

        var updatedTune = alternateTitle
            .Map(t => tune.AddAlternateTitle(t));

        updatedTune
            .Succ(async t =>
            {
                await _tuneRepository.UpdateAsync(tune.Id);
            })
            .Fail(e =>
            {
                return e.AsTask();
            });

        return updatedTune;
    }
}