using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace TL.CQRS.TuneCQ.Commands;

public class AddAlternateTitleCommand : IRequest<Option<Validation<Error, Unit>>>
{
    public int Id { get; set; }
    public string AlternateTitle { get; }

    public AddAlternateTitleCommand(int id, string alternateTitle)
    {
        Id = id;
        AlternateTitle = alternateTitle;
    }
}

public class AddAlternateTitleCommandHandler : IRequestHandler<AddAlternateTitleCommand, Option<Validation<Error, Unit>>>
{
    private readonly ITuneRepository _tuneRepository;

    public AddAlternateTitleCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Option<Validation<Error, Unit>>> Handle
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