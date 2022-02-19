using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using TL.Domain.ValueObjects.TrackValueObjects;

namespace TL.Domain;

public class Track
{
    private Track(){}
    
    public int Id { get; private set; }
    public TrackTitle Title { get; private set; }
    public TrkNumber TrkNumber { get; private set; }
    
    private List<TrackTune> _trackTunes = new List<TrackTune>();
    public IReadOnlyList<TrackTune> TrackTunes => _trackTunes;

    public int AlbumId { get; set; }
    public Album Album { get; set; }

    public static Track Create(TrackTitle title, TrkNumber trkNumber, Album album)
    {
        var track = new Track()
        {
            Title = title,
            TrkNumber = trkNumber,
            AlbumId = album.Id
        };
        
        return track;
    }

    public static Validation<Error, Track> CreateAndValidateTrackNumber(TrackTitle title, TrkNumber trkNumber, Album album)
    {
        var track = new Track()
        {
            Title = title,
            TrkNumber = trkNumber,
            AlbumId = album.Id
        };

        var duplicateTrackNumber = album.TrackListing
            .Exists(x => x.TrkNumber.Value == trkNumber.Value);

        if (duplicateTrackNumber)
            return Fail<Error, Track>("Track number already exists");

        return Success<Error, Track>(track);
    }



    public Unit Update(TrackTitle title, TrkNumber trkNumber)
    {
        Title = title;
        TrkNumber = trkNumber;
        return Unit.Default;
    }
    
    
}