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

    public static Track CreateTrack(TrackTitle title, TrackNumber trackNumber)
    {
        var track = new Track()
        {
            Title = title,
            TrackNumber = trackNumber
        };

        //add validation to prevent tracks with the same track number from being added
        //add validation for tracknumber
        
        return track;
    }
    
    public void AddTune(TrackTune trackTune)
    {
        if (_trackTunes.Contains(trackTune))
            throw new InvalidOperationException("The specified tune already exists on the track");
        
        _trackTunes.Add(trackTune);
    }
    
    public void RemoveTune(TrackTune trackTune)
    {
        if (!_trackTunes.Contains(trackTune))
            throw new InvalidOperationException("The specified tune was not found on the track");
        
        _trackTunes.Remove(trackTune);
    }

    public void Update(TrackTitle title, TrackNumber trackNumber)
    {
        Title = title;
        TrackNumber = trackNumber;
    }

    public void SetAlbumId(int id)
    {
        AlbumId = id;
    }
    
}