using TL.Domain;

namespace TL.Api.ArtistDTOs;

public class PostArtistDTO
{
    public string Name { get; set; }

    public static Artist ToArtist(PostArtistDTO dto)
    {
        var artist = Artist.CreateArtist(dto.Name);
        return artist;
    }
}
