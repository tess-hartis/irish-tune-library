using TL.Domain.ValueObjects.TrackTuneValueObjects;

namespace TL.Domain;

public class TrackTune
{
    public int Id { get; }
    public string Title { get; private set; }
    public TrackTuneOrder Order { get; private set; }
    
    public int TrackId { get; private set; }
    public Track Track { get; private set; }
    
    public int TuneId { get; private set; }
    public Tune Tune { get; private set; }
    
    
    public static TrackTune Create(TrackTuneOrder order)
    {
        var trackTune = new TrackTune
        {
            Order = order,
        };

        return trackTune;
    }

    public void SetTrack(Track track)
    {
        if (track.TrackTunes.Contains(this))
        {
            Track = track;
            TrackId = track.Id;
        }
    }

    public void SetTune(Tune tune)
    {
        var trackTunes = tune.FeaturedOnTrack
            .SelectMany(t => t.TrackTunes);
        
        if (trackTunes.Contains(this))
        {
            Tune = tune;
            TuneId = tune.Id;
            Title = tune.Title.Value;
        }
    }
}




