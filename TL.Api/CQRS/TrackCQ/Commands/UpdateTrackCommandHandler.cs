using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;
using Unit = LanguageExt.Unit;

namespace TL.Api.CQRS.TrackCQ.Commands;

public class UpdateTrackCommand : IRequest<Option<Validation<Error, Unit>>>
{
    public int Id { get; set; }
    public string Title { get; }
    public int TrackNumber { get; }

    public UpdateTrackCommand(int id, string title, int trackNumber)
    {
        Id = id;
        Title = title;
        TrackNumber = trackNumber;
    }
}
public class UpdateTrackCommandHandler : 
    IRequestHandler<UpdateTrackCommand, Option<Validation<Error, Unit>>>
{
    private readonly ITrackRepository _trackRepository;

    public UpdateTrackCommandHandler(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<Option<Validation<Error, Unit>>> Handle
        (UpdateTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await _trackRepository.FindAsync(command.Id);

        var title = TrackTitle.Create(command.Title);
        var number = TrkNumber.Create(command.TrackNumber);

        var updatedTrack = track
            .Map(t => (title, number)
                .Apply((x, y) => t.Update(x, y)));

        ignore(updatedTrack
            .Map(t =>
                t.Map(async x => await _trackRepository.SaveAsync())));

        return updatedTrack;

    }
}