using TL.Common;
using TL.Domain.Exceptions;
using TL.Domain.Validators;

namespace TL.Domain;

public class Artist
{
    private Artist(){ }
    public int Id { get; private set; }
    public string Name { get; private set; }
    private List<Album> _albums = new List<Album>();
    public IReadOnlyList<Album> Albums => _albums;

    public static Artist CreateArtist(string name)
    {
        var artist = new Artist
        {
            Name = name
        };
        
        var validator = new ArtistValidator();
        var errors = new List<string>();
        var results = validator.Validate(artist);
        
        if (results.IsValid == false)
        {
            foreach (var validationFailure in results.Errors)
            {
                errors.Add($"{validationFailure.ErrorMessage}");
            }
            
            throw new InvalidEntityException(string.Join(", ", errors));
        }
        return artist;
    }
    
    public void UpdateName(string name)
    {
        var errors = new List<string?>();
        
        if (string.IsNullOrWhiteSpace(name))
            errors.Add("title cannot be empty");
        
        if (name.Length < 2)
            errors.Add("title must be between 2 and 75 characters");
        
        if (name.Length > 75)
            errors.Add("title must be between 2 and 75 characters");

        if (errors.Any())
            throw new InvalidEntityException(string.Join(", ", errors));
        
        Name = name;
    }
    
}