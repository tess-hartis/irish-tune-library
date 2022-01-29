using Microsoft.VisualBasic;
using TL.Common;

namespace TL.Domain;

public class Album
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public int Year { get; private set; }

    private List<Artist> _artists = new List<Artist>();
    public IReadOnlyList<Artist> Artists => _artists;

    private List<Track> _tracks = new List<Track>();
    public IReadOnlyList<Track> TrackListing => _tracks;
    
    
    public static Album CreateAlbum(string title, int year)
    {
        var album = new Album()
        {
            Title = title,
            Year = year
        };

        if (string.IsNullOrWhiteSpace(title))
            throw new FormatException("Album title cannot be empty");

        if (!year.IsValidYear())
            throw new FormatException("Invalid year");
        
        return album;
    }

    private Album(){ }

    public Album(string title, int year)
    {
        CreateAlbum(title, year);
    }

    public void AddArtist(Artist artist)
    {
        _artists.Add(artist);
    }

    public void RemoveArtist(int artistId)
    {
        var artist = _artists.SingleOrDefault(x => x.Id == artistId);

        if (artist == null)
            throw new InvalidOperationException("The artist was not found");
        
        _artists.Remove(artist);
    }

    public void AddTrack(Track track)
    {
        _tracks.Add(track);
    }

    public void RemoveTrack(int trackId)
    {
        if (_tracks == null)
            throw new InvalidOperationException("Tracks collection not loaded");

        var track = _tracks.SingleOrDefault(
            x => x.Id == trackId);

        if (track == null)
            throw new InvalidOperationException("The track was not found");

        _tracks.Remove(track);
    }
    
    public void UpdateTitle(string title)
    {

        if (string.IsNullOrWhiteSpace(title))
            throw new FormatException("Album title cannot be empty");
        
        if (title.Length > 75)
            throw new FormatException("Album title must be 75 characters or fewer");
        
        Title = title;
    }

    public void UpdateYear(int year)
    {
        if (!year.IsValidYear())
            throw new FormatException("Invalid year");

        Year = year;
        
    }
    
    
}