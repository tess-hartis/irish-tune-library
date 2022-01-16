using TL.Common;

namespace TL.Domain;

public class Track
{
    public int Id { get; set; }
    public int TrackNumber { get; set; }
    public string Title { get; set; }
    public List<Tune> TuneList { get; set; }

    public Track(string title, int trackNumber)
    {
        if (title.IsValidNameOrTitle() & trackNumber.IsValidInt())
        {
            Id = new int();
            Title = title;
            TuneList = new List<Tune>();
            TrackNumber = trackNumber;
        }
        else
        {
            throw new FormatException();
        }
    }

    public Track()
    {
        
    }

    public void AddTune(Tune tune)
    {
        TuneList.Add(tune);
    }
}