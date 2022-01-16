using TL.Common;

namespace TL.Domain;

public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Album> Albums { get; set; }

    public Artist(string name)
    {
        if (name.IsValidNameOrTitle())
        {
            Name = name;
            Id = new int();
            Albums = new List<Album>();
        }
        else
        {
            throw new FormatException();
        }
        
    }
}