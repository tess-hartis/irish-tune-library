using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;

namespace TL.Api.ArtistDTOs;

public class PostArtistDTO
{
    public string Name { get; set; }

    public static Artist ToArtist(PostArtistDTO dto)
    {
        var name = ArtistName.Create(dto.Name);
        var artist = Artist.CreateArtist(name);
        return artist;
    }
}
