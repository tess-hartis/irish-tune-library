using TL.Domain;

namespace TL.Api.ArtistDTOs;

public class GetArtistDTO
{
    public string Name { get; set; }

    public static GetArtistDTO FromArtist(Artist artist)
    {
        return new GetArtistDTO
        {
            Name = artist.Name
        };
    }
}