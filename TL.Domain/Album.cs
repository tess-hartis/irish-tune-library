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
        if (_artists.Contains(artist))
            throw new InvalidOperationException
                ("The specified artist already exists on the album");
        
        _artists.Add(artist);
    }

    public void RemoveArtist(Artist artist)
    {
        if (!_artists.Contains(artist))
            throw new InvalidOperationException
                ("The specified artist was not found on the album");
        
        _artists.Remove(artist);
    }

    public void AddTrack(Track track)
    {
        _tracks.Add(track);
    }

    public void RemoveTrack(Track track)
    {
        if (!_tracks.Contains(track))
            throw new InvalidOperationException
                ("The specified track was not found on the album");
        
        _tracks.Remove(track);
    }
    
    public void Update(string title, int year)
    {
        var errors = new List<string?>();
        
        if (string.IsNullOrWhiteSpace(title))
            errors.Add("title cannot be empty");
        
        if (title.Length < 2)
            errors.Add("title must be between 2 and 75 characters");
        
        if (title.Length > 75)
            errors.Add("title must be between 2 and 75 characters");
        
        if (!year.ValidYear())
            errors.Add("invalid year");
        
        if (errors.Any())
            throw new InvalidEntityException(string.Join(", ", errors));
        
        Title = title;
    }

    
    
    
}