namespace TL.Domain;

public class TuneOnTrack
{
    public int Id { get; set; }
    public int TrackId { get; set; }
    public int TuneId { get; set; }
    public Track Track { get; set; }
    public Tune Tune { get; set; }
    public int Order { get; private set; }


    public static TuneOnTrack Create(int trackId, int tuneId, int order)
    {
        var tuneOnTrack = new TuneOnTrack
        {
            TrackId = trackId,
            TuneId = tuneId,
            Order = order
        };

        return tuneOnTrack;
    }
}




