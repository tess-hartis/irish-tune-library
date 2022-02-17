using LanguageExt;
using TL.Domain.Exceptions;
using TL.Domain.ValueObjects.AlbumValueObjects;

namespace TL.Domain;

public class Album
{
    public int Id { get; private set; }
    public AlbumTitle Title { get; private set; }
    public AlbumYear Year { get; private set; }
   
    
    private List<Artist> _artists = new List<Artist>();
    public IEnumerable<Artist> Artists => _artists;
    
    private List<Track> _tracks = new List<Track>();
    public IEnumerable<Track> TrackListing => _tracks;
    
    
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
    
    public Album AddArtist(Artist artist)
    {
        _artists.Add(artist);
        return this;
    }

    public Album RemoveArtist(Artist artist)
    {
        _artists.Remove(artist);
        return this;
    }

    public Album Update(AlbumTitle title, AlbumYear year)
    {
        Title = title;
        Year = year;
        return this;
    }




}