using TL.Domain.Exceptions;
using TL.Domain.ValueObjects.ArtistValueObjects;

namespace TL.Domain;

public class Artist
{
    private Artist(){ }
    
    public int Id { get; private set; }
    
    public ArtistName Name { get; private set; }
    
    private List<Album> _albums = new List<Album>();
    public IEnumerable<Album> Albums => _albums;

    public static Artist CreateArtist(ArtistName name)
    {
        var artist = new Artist()
        {
            Name = name
        };

        return artist;
    }
    
    public Artist Update(ArtistName name)
    {
        Name = name;
        return this;
    }
    
}