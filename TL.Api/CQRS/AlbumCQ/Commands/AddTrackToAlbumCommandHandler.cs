using LanguageExt;
using LanguageExt.Common;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class AddTrackToAlbumCommand : IRequest<Validation<Error, Track>>
{
    public int AlbumId { get; set; }
    public string Title { get; }
    public int TrackNumber { get; }

    public AddTrackToAlbumCommand(int albumId, string title, int trackNumber)
    {
        AlbumId = albumId;
        Title = title;
        TrackNumber = trackNumber;
    }
}
public class AddTrackToAlbumCommandHandler : 
    IRequestHandler<AddTrackToAlbumCommand, Validation<Error, Track>>
{
    private readonly IAlbumTrackService _albumTrackService;

    public AddTrackToAlbumCommandHandler(IAlbumTrackService albumTrackService)
    {
        _albumTrackService = albumTrackService;
    }

    public async Task<Validation<Error, Track>> Handle
        (AddTrackToAlbumCommand command, CancellationToken cancellationToken)
    {
        var title = TrackTitle.Create(command.Title);
        var number = TrackNumber.Create(command.TrackNumber);
        
        var track = (title, number)
            .Apply((t, n) =>
                Track.Create(t, n));

        await track
            .Succ(async t =>
            {
                await _albumTrackService.AddNewTrackToAlbum(command.AlbumId, t);
            })
            .Fail(e => e.AsTask());

        return track;
    }
}