using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Commands;

public class UpdateTrackCommand : IRequest<Validation<Error, Track>>
{
    public int Id { get; set; }
    public string Title { get; }
    public int Number { get; }

    public UpdateTrackCommand(int id, string title, int number)
    {
        Id = id;
        Title = title;
        Number = number;
    }
}
public class UpdateTrackCommandHandler : 
    IRequestHandler<UpdateTrackCommand, Validation<Error, Track>>
{
    private readonly ITrackRepository _trackRepository;

    public UpdateTrackCommandHandler(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<Validation<Error, Track>> Handle
        (UpdateTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await _trackRepository.FindAsync(command.Id);

        var title = TrackTitle.Create(command.Title);
        var number = TrackNumber.Create(command.Number);

        var updatedTrack = (title, number)
            .Apply((t, n) => track.Update(t, n));

        await updatedTrack
            .Succ(async t =>
            {
                await _trackRepository.UpdateAsync(track.Id);
            })
            .Fail(e => e.AsTask());

        return updatedTrack;
    }
}