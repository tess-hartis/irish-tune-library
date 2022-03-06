using LanguageExt;
using TL.Domain.ValueObjects.ArtistValueObjects;

namespace TL.Domain;

public class Artist
{
    private Artist(){ }
    
    public int Id { get; }
    public ArtistName Name { get; private set; }
    
    private readonly List<Album> _albums = new List<Album>();
    public IEnumerable<Album> Albums => _albums;

    public static Artist Create(ArtistName name)
    {
        var artist = new Artist()
        {
            Name = name
        };

        return artist;
    }
    
    public Unit Update(ArtistName name)
    {
        Name = name;
        return Unit.Default;
    }
}