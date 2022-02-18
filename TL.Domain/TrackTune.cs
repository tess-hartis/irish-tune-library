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
    
    
    public static TrackTune Create(Track track, Tune tune, TrackTuneOrder order)
    {
        var trackTune = new TrackTune
        {
            TrackId = track.Id,
            TuneId = tune.Id,
            Order = order,
            Title = tune.Title.Value
        };

        return trackTune;
    }
    
}




