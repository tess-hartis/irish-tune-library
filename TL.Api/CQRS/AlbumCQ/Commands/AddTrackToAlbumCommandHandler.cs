using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class AddTrackToAlbumCommand : IRequest<Option<Validation<Error, Track>>>
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
    IRequestHandler<AddTrackToAlbumCommand, Option<Validation<Error, Track>>>
{
    private readonly ITrackRepository _trackRepository;
    private readonly IAlbumRepository _albumRepository;

    public AddTrackToAlbumCommandHandler(ITrackRepository trackRepository, IAlbumRepository albumRepository)
    {
        _trackRepository = trackRepository;
        _albumRepository = albumRepository;
    }

    public async Task<Option<Validation<Error, Track>>> Handle
        (AddTrackToAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.FindAsync(command.AlbumId);
        var title = Some(TrackTitle.Create(command.Title));
        var trackNumber = Some(TrackNumber.Create(command.TrackNumber));

        var result =
            from a in album
            from t in title
            from tn in trackNumber
            select (t, tn).Apply(((x, y) => Track.Create(x, y, a)));

        ignore(result
            .Map(t =>
                t.Map(async x => await _trackRepository.AddAsync(x))));

        return result;
        
    }
}