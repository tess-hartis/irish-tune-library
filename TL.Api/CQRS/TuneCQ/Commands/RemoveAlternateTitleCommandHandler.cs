using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class RemoveAlternateTitleCommand : IRequest<Option<Tune>>
{
    public int Id { get; set; }
    public string AltTitleString { get; }

    public RemoveAlternateTitleCommand(int id, string altTitleString)
    {
        Id = id;
        AltTitleString = altTitleString;
    }
}
public class RemoveAlternateTitleCommandHandler : 
    IRequestHandler<RemoveAlternateTitleCommand, Option<Tune>>
{
    private readonly ITuneRepository _tuneRepository;

    public RemoveAlternateTitleCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Option<Tune>> Handle
        (RemoveAlternateTitleCommand command, CancellationToken cancellationToken)
    {
        
        var tune = await _tuneRepository.FindAsync(command.Id);

        var possibleTitle = tune
            .Map(t => t.AlternateTitles
                .First(x => x.Value == command.AltTitleString));

        var result =
            from t in tune
            from p in possibleTitle
            select t.RemoveAlternateTitle(p);

        ignore(result.Map(async x => await _tuneRepository.UpdateAsync(x)));

        return result;

        // var tune = await _tuneRepository.FindAsync(command.Id);
        //
        // var unvalidated = command.AlternateTitle;
        //
        // var hmm = tune.Map(t =>
        // {
        //     if (t.AlternateTitles.Exists(x => x.Value == unvalidated))
        //         
        //         return t.RemoveAlternateTitle(unvalidated);
        //     return t;
        // });
        // var updatedTune = tune.Map(t => t.RemoveAlternateTitle(unvalidated));
        //
        // tune.Map(t => hmm.Map(h =>
        // {
        //     if (t.AlternateTitles.Count > h.AlternateTitles.Count)
        //         ignore(updatedTune
        //             .Map(async x => await _tuneRepository.UpdateAsync(x)));
        // }));
        //
        //
        // return updatedTune;

        // ignore(tune.Map(async x => await _tuneRepository.RemoveAlternateTitle(x, possibleTitleToDelete)));




    }
}