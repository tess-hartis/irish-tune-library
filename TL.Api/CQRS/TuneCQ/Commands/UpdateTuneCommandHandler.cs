using CSharpFunctionalExtensions;
using LanguageExt;
using LanguageExt.ClassInstances.Pred;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class UpdateTuneCommand : IRequest<Validation<Error, Tune>>
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

public class UpdateTuneCommandHandler : IRequestHandler<UpdateTuneCommand, Validation<Error, Tune>>
{
    private readonly ITuneRepository _tuneRepository;

    public UpdateTuneCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Validation<Error, Tune>> Handle(UpdateTuneCommand command, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(command.Id);
        
        var title = TuneTitle.Create(command.Title);
        var composer = TuneComposer.Create(command.Composer);
        
        var updatedTune = (title, composer)
            .Apply((t, c) => tune.Update(t,
                c, command.Type, command.Key));

        updatedTune
            .Succ(async t =>
            {
                await _tuneRepository.UpdateAsync(command.Id);
            })
            .Fail(e =>
            {
                return e.AsTask();
            });

        return updatedTune;

    }
}