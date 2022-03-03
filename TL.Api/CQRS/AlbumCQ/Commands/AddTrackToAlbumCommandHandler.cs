using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using MediatR;
using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class AddTrackToAlbumCommand : IRequest
    <Option<Validation<Error, bool>>>
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
    IRequestHandler<AddTrackToAlbumCommand, Option<Validation<Error, bool>>>
{
    private readonly ITrackAlbumUnitOfWork _unitOfWork;

    public AddTrackToAlbumCommandHandler(ITrackAlbumUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Option<Validation<Error, bool>>> Handle
        (AddTrackToAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _unitOfWork.AlbumRepo.FindAsync(command.AlbumId);
        var title = Some(TrackTitle.Create(command.Title));
        var trackNumber = Some(TrkNumber.Create(command.TrackNumber));

        var track =
            from tt in title
            from tn in trackNumber
            from a in album
            select (tt, tn).Apply(Track.Create)
                .Map(a.AddTrack);
        
        ignore(track.MapT(async _ => await _unitOfWork.Save()));

        return track;
    }
}

