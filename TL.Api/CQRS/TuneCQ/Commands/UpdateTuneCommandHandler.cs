using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TL.Api.DTOs.TuneDTOS;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class UpdateTuneCommand : IRequest<GetTuneDTO>
{
    public int Id { get; }
    public TuneTitle Title { get; }
    public TuneComposer Composer { get; }
    public string Type { get; }
    public string Key { get; }

    public UpdateTuneCommand(int id, TuneTitle title, TuneComposer composer, string type, string key)
    {
        Id = id;
        Title = title;
        Composer = composer;
        Type = type;
        Key = key;
    }
}

public class UpdateTuneCommandHandler : IRequestHandler<UpdateTuneCommand, GetTuneDTO>
{
    private readonly ITuneRepository _tuneRepository;

    public UpdateTuneCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<GetTuneDTO> Handle(UpdateTuneCommand command, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(command.Id);
        await _tuneRepository.UpdateTune
            (tune.Id, command.Title, command.Composer, command.Type, command.Key);
        return GetTuneDTO.FromTune(tune);

    }
}