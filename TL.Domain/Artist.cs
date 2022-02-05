using TL.Domain.Exceptions;
using TL.Domain.Validators;
using TL.Domain.ValueObjects.ArtistValueObjects;

namespace TL.Domain;

public class Artist
{
    private Artist(){ }
    
    public int Id { get; private set; }
    
    public ArtistName Name { get; private set; }
    
    private List<Album> _albums = new List<Album>();
    public IReadOnlyList<Album> Albums => _albums;

    public static Artist CreateArtist(ArtistName name)
    {
        var artist = new Artist()
        {
            Name = name
        };
        
        // var validator = new ArtistValidator();
        // var errors = new List<string>();
        // var results = validator.Validate(artist);
        //
        // if (results.IsValid == false)
        // {
        //     foreach (var validationFailure in results.Errors)
        //     {
        //         errors.Add($"{validationFailure.ErrorMessage}");
        //     }
        //     
        //     throw new InvalidEntityException(string.Join(", ", errors));
        // }
        
        return artist;
    }
    
    public void UpdateName(ArtistName name)
    {
        Name = name;
    }
    
}