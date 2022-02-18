using CSharpFunctionalExtensions;
using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.ClassInstances.Pred;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class UpdateTuneCommand : IRequest<Option<Validation<Error, Tune>>>
{
    public int Id { get; set; }
    public string Title { get; }
    public string Composer { get; }
    public string Type { get; }
    public string Key { get; }

    public UpdateTuneCommand(int id, string title, string composer, string type, string key)
    {
        Id = id;
        Title = title;
        Composer = composer;
        Type = type;
        Key = key;
    }
}

public class UpdateTuneCommandHandler : IRequestHandler<UpdateTuneCommand, Option<Validation<Error, Tune>>>
{
    private readonly ITuneRepository _tuneRepository;

    public UpdateTuneCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Option<Validation<Error, Tune>>> Handle(UpdateTuneCommand command, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(command.Id);

        var title = TuneTitle.Create(command.Title);
        var composer = TuneComposer.Create(command.Composer);
        var type = TuneTypeValueObj.Create(command.Type);
        var key = TuneKeyValueObj.Create(command.Key);

        var updatedTune = tune
            .Map(t => (title, composer, type, key)
                .Apply((x, y, ty, k) =>
                    t.Update(x, y, ty, k)));

        ignore(updatedTune
            .Map(t =>
                t.Map(async x => await _tuneRepository.UpdateAsync(x))));

        return updatedTune;
        
    }
}