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
    
    
    public static TrackTune Create(int trackId, int tuneId, TrackTuneOrder order)
    {
        var trackTune = new TrackTune
        {
            TrackId = trackId,
            TuneId = tuneId,
            Order = order
            
        };

        return trackTune;
    }

    public void SetTuneId(int id)
    {
        TuneId = id;
    }

    public void SetTrackId(int id)
    {
        TrackId = id;
    }

    public void SetTune(Tune tune)
    {
        Tune = tune;
    }

    public void SetTrack(Track track)
    {
        Track = track;
    }

    public void SetTitle(string title)
    {
        Title = title;
    }
}




