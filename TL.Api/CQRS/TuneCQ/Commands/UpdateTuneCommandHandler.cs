using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;
using MediatR;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;
using Unit = LanguageExt.Unit;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class UpdateTuneCommand : IRequest<Option<Validation<Error, Unit>>>
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

public class UpdateTuneCommandHandler : IRequestHandler<UpdateTuneCommand, Option<Validation<Error, Unit>>>
{
    private readonly ITuneRepository _tuneRepository;

    public UpdateTuneCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Option<Validation<Error, Unit>>> Handle(UpdateTuneCommand command, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(command.Id);

        var title = TuneTitle.Create(command.Title);
        var composer = TuneComposer.Create(command.Composer);
        var type = TuneTypeValueObj.Create(command.Type);
        var key = TuneKeyValueObj.Create(command.Key);

        var updatedTune = tune
            .Map(t => (title, composer, type, key)
                .Apply(t.Update));

        ignore(updatedTune
            .Map(t =>
                t.Map(async x => await _tuneRepository.SaveAsync())));

        return updatedTune;
    }
}