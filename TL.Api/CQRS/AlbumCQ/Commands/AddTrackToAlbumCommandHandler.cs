using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class AddTrackToAlbumCommand : IRequest<GetTrackDTO>
{
    public int AlbumId { get; }
    public TrackTitle Title { get; }
    public TrackNumber TrackNumber { get; }

    public AddTrackToAlbumCommand(int albumId, TrackTitle title, TrackNumber trackNumber)
    {
        AlbumId = albumId;
        Title = title;
        TrackNumber = trackNumber;
    }
}
public class AddTrackToAlbumCommandHandler : IRequestHandler<AddTrackToAlbumCommand, GetTrackDTO>
{
    private readonly AlbumTrackService _albumTrackService;

    public AddTrackToAlbumCommandHandler(AlbumTrackService albumTrackService)
    {
        _albumTrackService = albumTrackService;
    }

    public async Task<GetTrackDTO> Handle(AddTrackToAlbumCommand command, CancellationToken cancellationToken)
    {
        
        var track = await _albumTrackService.AddNewTrackToAlbum(command.AlbumId, command.Title, command.TrackNumber);
        return GetTrackDTO.FromTrack(track);
    }
}