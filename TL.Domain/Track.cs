using LanguageExt;
using TL.Domain.ValueObjects.TrackValueObjects;

namespace TL.Domain;

public class Track
{
    private Track() { }
    
    public int Id { get; }
    public TrackTitle Title { get; private set; }
    public TrkNumber TrkNumber { get; private set; }
    
    private readonly List<TrackTune> _trackTunes = new List<TrackTune>();
    public IReadOnlyList<TrackTune> TrackTunes => _trackTunes;

    public int AlbumId { get; private set; }
    public Album Album { get; private set; }

    public static Track Create(TrackTitle title, TrkNumber trkNumber)
    {
        var track = new Track()
        {
            Title = title,
            TrkNumber = trkNumber,
        };
        
        return track;
    }
    
    public Unit Update(TrackTitle title, TrkNumber trkNumber)
    {
        Title = title;
        TrkNumber = trkNumber;
        return Unit.Default;
    }

    public void SetAlbum(Album album)
    {
        if (album.TrackListing.Contains(this))
        {
            Album = album;
            AlbumId = album.Id;
        }
    }

    public bool AddTrackTune(TrackTune trackTune, Tune tune)
    {
        var duplicateOrder = _trackTunes.Exists(x =>
            x.Order.Value == trackTune.Order.Value);

        if (duplicateOrder)
            return false;
        
        _trackTunes.Add(trackTune);
        trackTune.SetTrack(this);
        trackTune.SetTune(tune);
        return true;
    }
}