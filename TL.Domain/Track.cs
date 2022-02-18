using LanguageExt;
using TL.Domain.ValueObjects.TrackValueObjects;

namespace TL.Domain;

public class Track
{
    private Track(){}
    
    public int Id { get; private set; }
    public TrackTitle Title { get; private set; }
    public TrackNumber TrackNumber { get; private set; }
    
    private List<TrackTune> _trackTunes = new List<TrackTune>();
    public IReadOnlyList<TrackTune> TrackTunes => _trackTunes;

    public int AlbumId { get; set; }
    public Album Album { get; set; }

    public static Track Create(TrackTitle title, TrackNumber trackNumber, Album album)
    {
        var track = new Track()
        {
            Title = title,
            TrackNumber = trackNumber,
            AlbumId = album.Id
        };

        //add validation to prevent tracks with the same track number from being added
        //add validation for tracknumber
        
        return track;
    }
    
    public Track AddTune(TrackTune trackTune)
    {
        _trackTunes.Add(trackTune);
        return this;
    }
    
    public Unit RemoveTune(TrackTune trackTune)
    {
        _trackTunes.Remove(trackTune);
        return Unit.Default;
    }

    public Unit Update(TrackTitle title, TrackNumber trackNumber)
    {
        Title = title;
        TrackNumber = trackNumber;
        return Unit.Default;
    }
    
    
}