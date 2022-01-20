using TL.Common;

namespace TL.Domain;

public class Track
{
    private Track(){}
    public int Id { get; private set; }
    public string Title { get; private set; }
    public int TrackNumber { get; private set; }
    
    private HashSet<Tune> _tuneList;
    public IReadOnlyCollection<Tune> TuneList => _tuneList.ToList();

    public static Track CreateTrack(string title, int trackNumber)
    {
        var track = new Track()
        {
            Title = title,
            TrackNumber = trackNumber
        };

        if (string.IsNullOrWhiteSpace(title))
            throw new FormatException("Track title cannot be empty");

        if (!trackNumber.IsValidTrackNumber())
            throw new FormatException("Invalid track number");

        return track;
    }

    internal Track(string title, int trackNumber)
    {
        CreateTrack(title, trackNumber);
    }

    public void AddTune(string title, TuneTypeEnum type, 
        TuneKeyEnum key, string composer)
    {
        if (_tuneList == null)
            throw new InvalidOperationException("Tune collection was not loaded");

        _tuneList.Add(new Tune(title, type, key, composer));
    }

    public void RemoveTune(int tuneId)
    {
        if (_tuneList == null)
            throw new InvalidOperationException("Tune list was not loaded");

        var tune = _tuneList.SingleOrDefault(
            x => x.Id == tuneId);

        if (tune == null)
            throw new InvalidOperationException("The tune was not found");

        _tuneList.Remove(tune);
    }
    
    public void UpdateTitle(string title)
    {
        
        if (string.IsNullOrWhiteSpace(title))
            throw new FormatException("Track title cannot be empty");
        
        if (title.Length > 75)
            throw new FormatException("Track title must be 75 characters or fewer");
        
        Title = title;
    }

    public void UpdateTrackNumber(int trackNumber)
    {
        if (!trackNumber.IsValidTrackNumber())
            throw new FormatException("Invalid track number");

        TrackNumber = trackNumber;
    }
    
}