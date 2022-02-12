using TL.Domain.Exceptions;
using TL.Domain.Validators;
using TL.Domain.ValueObjects.AlbumValueObjects;

namespace TL.Domain;

public class Album
{
    public int Id { get; private set; }
    public AlbumTitle Title { get; private set; }
    public AlbumYear Year { get; private set; }
   
    
    private List<Artist> _artists = new List<Artist>();
    public IReadOnlyList<Artist> Artists => _artists;
    
    private List<Track> _tracks = new List<Track>();
    public IReadOnlyList<Track> TrackListing => _tracks;
    
    
    public static Album Create(AlbumTitle title, AlbumYear year)
    {
        var album = new Album()
        {
            Title = title,
            Year = year
        };
        
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

    public Album Update(AlbumTitle title, AlbumYear year)
    {
        Title = title;
        Year = year;
        return this;
    }




}