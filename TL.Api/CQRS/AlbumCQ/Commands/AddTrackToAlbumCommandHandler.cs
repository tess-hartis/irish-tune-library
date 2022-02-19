using LanguageExt;
using LanguageExt.Common;
using LanguageExt.SomeHelp;
using static LanguageExt.Prelude;
using MediatR;
using TL.Api.DTOs.TrackDTOs;
using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Api.CQRS.AlbumCQ.Commands;

public class AddTrackToAlbumCommand : IRequest
    <Option<Validation<Error, Option<Boolean>>>>
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
    IRequestHandler<AddTrackToAlbumCommand, Option<Validation<Error, Option<Boolean>>>>
{
    private readonly ITrackRepository _trackRepository;
    private readonly IAlbumRepository _albumRepository;

    public AddTrackToAlbumCommandHandler(ITrackRepository trackRepository, IAlbumRepository albumRepository)
    {
        _trackRepository = trackRepository;
        _albumRepository = albumRepository;
    }

    public async Task<Option<Validation<Error, Option<Boolean>>>> Handle
        (AddTrackToAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.FindAsync(command.AlbumId);
        var title = Some(TrackTitle.Create(command.Title));
        var trackNumber = Some(TrkNumber.Create(command.TrackNumber));

        var track =
            from tt in title
            from tn in trackNumber
            select (tt, tn).Apply(((x, y) => Track.Create(x, y)))
                .Map(t => album
                    .Map(a => 
                        a.AddTrack(t)));
        
        
        ignore(track.MapT(x => 
            x.Map(async y => await _trackRepository.SaveAsync())));

        return track;

    }
    
    //ATTEMPT #1 (does not validate track number)
    // public async Task<Option<Validation<Error, Track>>> Handle
    //     (AddTrackToAlbumCommand command, CancellationToken cancellationToken)
    // {
    //     var album = await _albumRepository.FindAsync(command.AlbumId);
    //     var title = Some(TrackTitle.Create(command.Title));
    //     var trackNumber = Some(TrkNumber.Create(command.TrackNumber));
    //     
    //     var result =
    //         from a in album
    //         from t in title
    //         from tn in trackNumber
    //         select (t, tn).Apply(((x, y) => Track.Create(x, y, a)));
    //     
    //         ignore(result.MapT(async x => await _trackRepository.AddAsync(x)));
    //
    //     return result;
    //     
    // }
    
    //ATTEMPT #2 (works)
    // public async Task<Option<Validation<Error, Validation<Error, Track>>>> Handle
    //     (AddTrackToAlbumCommand command, CancellationToken cancellationToken)
    // {
    //     var album = await _albumRepository.FindAsync(command.AlbumId);
    //     var title = Some(TrackTitle.Create(command.Title));
    //     var trackNumber = Some(TrkNumber.Create(command.TrackNumber));
    //     
    //     var result =
    //         from a in album
    //         from t in title
    //         from tn in trackNumber
    //         select (t, tn).Apply(((x, y) => Track.CreateAndValidateTrackNumber(x, y, a)));
    //     
    //     ignore(result.MapT(x => 
    //         x.Map(async y => await _trackRepository.AddAsync(y))));
    //
    //     return result;
    //     
    // }
}

// This is what we want to do:
// var track = track.create()
// album.addTrack(track);

// ============
// Album
// AddTrack(Track track) {
// check the list
// _tracks.add(track);
// track.SetAlbum(this);
// }
// ============

// ============
// Track
// SetAlbum(Album album) {
// if (album.TrackListings.Contains(blah blah blah)
// track.Album = this;
// track.AlbumId = this.Id;
// }
// ============