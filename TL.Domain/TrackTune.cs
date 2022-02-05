namespace TL.Domain;

public class TrackTune
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Order { get; private set; }
    
    public int TrackId { get; set; }
    public Track Track { get; set; }
    
    public int TuneId { get; set; }
    public Tune Tune { get; set; }
    
    
    public static TrackTune Create(int trackId, int tuneId, int order)
    {
        var trackTune = new TrackTune
        {
            TrackId = trackId,
            TuneId = tuneId,
            Order = order
            
        };

        var errors = new List<string>();
        
        if (order > 25 || order < 1)
            errors.Add("Tune order must be between 1 and 25");

        if (errors.Any())
            throw new InvalidEntityException("invalid tune order");

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




