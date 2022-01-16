using TL.Common;

namespace TL.Domain;

public class Album
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public int Year { get; set; }
    public List<Artist> Artists { get; set; }
    public List<Track> TrackListing { get; set; }
    
    


    public Album(string title, int year)
    {
        if (title.IsValidNameOrTitle() && year.IsValidYear())
        {
            Id = new int();
            Title = title;
            Artists = new List<Artist>();
            TrackListing = new List<Track>();
            Year = year;
        }
        else
        {
            throw new FormatException();
        }
    }

    public void AddArtist(Artist artist)
    {
        Artists.Add(artist);
    }

    public void AddTrack(Track track)
    {
        TrackListing.Add(track);
    }
}