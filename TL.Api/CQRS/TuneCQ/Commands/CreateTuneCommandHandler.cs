using LanguageExt.Common;
using MediatR;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class CreateTuneCommand : IRequest<GetTuneDTO>
{
    public TuneTitle Title { get; }
    public TuneComposer Composer { get; }
    public string Type { get; }
    public string Key { get; }

    public CreateTuneCommand(TuneTitle title, TuneComposer composer, string type, string key)
    {
        Title = title;
        Composer = composer;
        Type = type;
        Key = key;
    }
}

public class CreateTuneCommandHandler : IRequestHandler<CreateTuneCommand, GetTuneDTO>
{
    private readonly ITuneRepository _tuneRepository;

    public CreateTuneCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }
    

    public async Task<GetTuneDTO> Handle(CreateTuneCommand command, CancellationToken cancellationToken)
    {
        var tune = Tune.Create(command.Title, command.Composer, command.Type, command.Key);
        await _tuneRepository.AddAsync(tune);
        return GetTuneDTO.FromTune(tune);
    }
    
}