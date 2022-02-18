using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class AddAlternateTitleCommand : IRequest<Option<Validation<Error, Tune>>>
{
    public int Id { get; set; }
    public string AlternateTitle { get; }

    public AddAlternateTitleCommand(int id, string alternateTitle)
    {
        Id = id;
        AlternateTitle = alternateTitle;
    }
}
public class AddAlternateTitleCommandHandler : IRequestHandler<AddAlternateTitleCommand, Option<Validation<Error, Tune>>>
{
    private readonly ITuneRepository _tuneRepository;

    public AddAlternateTitleCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Option<Validation<Error, Tune>>> Handle
        (AddAlternateTitleCommand command, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(command.Id);
        var alternateTitle = TuneTitle.Create(command.AlternateTitle);

        var updatedTune = tune
            .Map(t => alternateTitle
                .Map(a => t.AddAlternateTitle(a)));

        ignore(updatedTune
            .Map(t =>
                t.Map(async x => await _tuneRepository.SaveAsync())));

        return updatedTune;


    }
}