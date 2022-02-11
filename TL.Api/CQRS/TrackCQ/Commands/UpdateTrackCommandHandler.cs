using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.TrackCQ.Commands;

public class UpdateTrackCommand : IRequest<GetTrackDTO>
{
    public int Id { get; }
    public TrackTitle Title { get; }
    public TrackNumber Number { get; }

    public UpdateTrackCommand(int id, TrackTitle title, TrackNumber number)
    {
        Id = id;
        Title = title;
        Number = number;
    }
}
public class UpdateTrackCommandHandler : IRequestHandler<UpdateTrackCommand, GetTrackDTO>
{
    private readonly ITrackRepository _trackRepository;

    public UpdateTrackCommandHandler(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<GetTrackDTO> Handle(UpdateTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await _trackRepository.FindAsync(command.Id);
        await _trackRepository.UpdateTrack(track.Id, command.Title, command.Number);
        return GetTrackDTO.FromTrack(track);
    }
}