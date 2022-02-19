using TL.Domain.ValueObjects.TrackTuneValueObjects;

namespace TL.Domain;

public class TrackTune
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TrackTuneOrder Order { get; private set; }
    
    public int TrackId { get; set; }
    public Track Track { get; set; }
    
    public int TuneId { get; set; }
    public Tune Tune { get; set; }
    
    
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
            this.Track = track;
            this.TrackId = track.Id;
        }
    }

    public void SetTune(Tune tune)
    {
        var trackTunes = tune.FeaturedOnTrack
            .SelectMany(t => t.TrackTunes);
        
        if (trackTunes.Contains(this))
        {
            this.Tune = tune;
            this.TuneId = tune.Id;
            this.Title = tune.Title.Value;
        }
    }
}




