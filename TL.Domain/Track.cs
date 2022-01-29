using TL.Common;

namespace TL.Domain;

public class Track
{
    private Track(){}
    public int Id { get; private set; }
    public string Title { get; private set; }
    public int TrackNumber { get; private set; }
    private List<Tune> _tuneList = new List<Tune>();
    public IReadOnlyList<Tune> TuneList => _tuneList;

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

    public void AddTune(Tune tune)
    {
        _tuneList.Add(tune);
    }
    
    public void RemoveTune(Tune tune)
    {
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