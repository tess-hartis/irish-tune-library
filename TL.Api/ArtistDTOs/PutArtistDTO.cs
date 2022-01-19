using TL.Domain;

namespace TL.Api.ArtistDTOs;

public class PutArtistDTO
{
    public string Name { get; set; }

    public static Artist UpdatedArtist(Artist artist, PutArtistDTO dto)
    {
        artist.Name = dto.Name;
        return artist;
    }
}