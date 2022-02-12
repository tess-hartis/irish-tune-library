using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class RemoveAlternateTitleCommand : IRequest<Validation<Error, Tune>>
{
    public int Id { get; set; }
    public string AlternateTitle { get; }

    public RemoveAlternateTitleCommand(int id, string alternateTitle)
    {
        Id = id;
        AlternateTitle = alternateTitle;
    }
}
public class RemoveAlternateTitleCommandHandler : 
    IRequestHandler<RemoveAlternateTitleCommand, Validation<Error, Tune>>
{
    private readonly ITuneRepository _tuneRepository;

    public RemoveAlternateTitleCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Validation<Error, Tune>> Handle
        (RemoveAlternateTitleCommand command, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(command.Id);
        var toDelete = TuneTitle.Create(command.AlternateTitle);

        var updatedTune = (toDelete)
            .Map(t => tune.RemoveAlternateTitle(t));

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