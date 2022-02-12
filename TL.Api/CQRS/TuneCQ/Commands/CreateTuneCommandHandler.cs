using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class CreateTuneCommand : IRequest<Validation<Error, Tune>>
{
    public string Title { get; }
    public string Composer { get; }
    public string Type { get; }
    public string Key { get; }

    public CreateTuneCommand(string title, string composer, string type, string key)
    {
        Title = title;
        Composer = composer;
        Type = type;
        Key = key;
    }
}

public class CreateTuneCommandHandler : IRequestHandler<CreateTuneCommand, Validation<Error, Tune>>
{
    private readonly ITuneRepository _tuneRepository;

    public CreateTuneCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }
    

    public async Task<Validation<Error, Tune>> Handle(CreateTuneCommand command, CancellationToken cancellationToken)
    {
        var title = TuneTitle.Create(command.Title);
        var composer = TuneComposer.Create(command.Composer);

        var newTune = (title, composer)
            .Apply((t, c) =>
                Tune.Create(t, c, command.Type, command.Key));

        await newTune
            .Succ(async toon =>
            {
                await _tuneRepository.AddAsync(toon);
                
            })
            .Fail(e =>
            {
                return e.AsTask();
            });

        return newTune;

    }
    
}