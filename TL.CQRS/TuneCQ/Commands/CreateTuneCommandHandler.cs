using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using MediatR;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.CQRS.TuneCQ.Commands;

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
        var type = TuneTypeValueObj.Create(command.Type);
        var key = TuneKeyValueObj.Create(command.Key);

        var newTune = (title, composer, type, key)
            .Apply(Tune.Create);

        await newTune
            .Succ(async toon =>
            {
                await _tuneRepository.AddAsync(toon);
                
            })
            .Fail(e => e.AsTask());

        return newTune;
    }
}