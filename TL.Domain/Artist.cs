using TL.Common;

namespace TL.Domain;

public class Artist
{
    private Artist(){ }
    public int Id { get; private set; }
    public string Name { get; private set; }

    public static Artist CreateArtist(string name)
    {
        var artist = new Artist
        {
            Name = name
        };

        if (string.IsNullOrWhiteSpace(name))
            throw new FormatException("Artist name cannot be empty");
        
        if (name.Length > 50)
            throw new FormatException("Artist name must be 50 characters or fewer");

        return artist;
    }
    
    internal Artist(string name)
    {
        CreateArtist(name);
    }

    public void UpdateName(string name)
    {
        if (!name.IsValidNameOrTitle())
            throw new FormatException("Invalid name");

        Name = name;
    }

    
}