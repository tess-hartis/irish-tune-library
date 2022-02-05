using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;

namespace TL.Api.ArtistDTOs;

public class PutArtistDTO
{
    public string Name { get; set; }

    public static Artist UpdatedArtist(Artist artist, PutArtistDTO dto)
    {
        var name = ArtistName.Create(dto.Name);
        artist.UpdateName(name);
        return artist;
    }
}