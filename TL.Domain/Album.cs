using Microsoft.VisualBasic;
using TL.Common;
using TL.Domain.Exceptions;
using TL.Domain.Validators;

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

        var validator = new AlbumValidator();
        var errors = new List<string>();
        var results = validator.Validate(album);
        
        if (results.IsValid == false)
        {
            foreach (var validationFailure in results.Errors)
            {
                errors.Add($"{validationFailure.ErrorMessage}");
            }
            
            throw new InvalidEntityException(string.Join(", ", errors));
        }
        
        return album;
    }

    private Album(){ }
    
    public void AddArtist(Artist artist)
    {
        _artists.Add(artist);
    }

    public void RemoveArtist(Artist artist)
    {
        _artists.Remove(artist);
    }

    public void AddTrack(Track track)
    {
        _tracks.Add(track);
    }

    public void RemoveTrack(Track track)
    {
        _tracks.Remove(track);
    }
    
    public void UpdateTitle(string title)
    {
        Title = title;
    }

    public void UpdateYear(int year)
    {
        Year = year;
    }
    
    
}