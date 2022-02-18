using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class RemoveAlternateTitleCommand : IRequest<Option<Boolean>>
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
    IRequestHandler<RemoveAlternateTitleCommand, Option<Boolean>>
{
    private readonly ITuneRepository _tuneRepository;

    public RemoveAlternateTitleCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Option<Boolean>> Handle
        (RemoveAlternateTitleCommand command, CancellationToken cancellationToken)
    {
        
        var tune = await _tuneRepository.FindAsync(command.Id);
        var possible = Some(command.AlternateTitle);
        
        var hmm =
            from t in tune
            from p in possible
            select t.RemoveAlternateTitle(t.AlternateTitles.FirstOrDefault(x => x.Value == p));
        
        ignore( hmm.Map(async x => await _tuneRepository.SaveAsync()));
        
        return hmm;

        // var tune = await _tuneRepository.FindAsync(command.Id);
        // var possible = tune.Map(t => t.AlternateTitles.FirstOrDefault(x => x.Value == command.AltTitleString));
        //
        //var result = tune.Map(t => possible.Map(x => t.RemoveAlternateTitle(x)));
        //
        // var result = tune.Map(t =>
        // {
        //     if (possible == null)
        //         return Option<Tune>.None;
        //
        //     return possible.Map(x => t.RemoveAlternateTitle(x));
        // });
        //
        // ignore(result.Map(x => x.Map(async y => await _tuneRepository.SaveAsync())));
        //
        // return result;


        // var result =
        //     from t in tune
        //     from p in possibleTitle
        //     select t.RemoveAlternateTitle(p);

        // ignore(result.Map(async x => await _tuneRepository.SaveAsync()));
        //
        // return result;

        // var tune = await _tuneRepository.FindAsync(command.Id);
        //
        // var unvalidated = command.AlternateTitle;
        //
        // var hmm = tune.Map(t =>
        // {
        //     if (t.AlternateTitles.Exists(x => x.Value == unvalidated))
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

        // var hmm = tune.Map(t =>
        // {
        //     if (t.AlternateTitles.Exists(x => x.Value == possible))
        //         return t.RemoveAlternateTitle(t.AlternateTitles.First(x => x.Value == possible));
        //
        //     return t;
        // });

        // var hmm = tune.Map(async t =>
        // {
        //     var myBool = t.RemoveAlternateTitle(t.AlternateTitles.FirstOrDefault(x => x.Value == possible));
        //     if (myBool)
        //     {
        //         await _tuneRepository.SaveAsync();
        //         return Some(t);
        //     }
        //
        //     return None;
        // });


    }
}