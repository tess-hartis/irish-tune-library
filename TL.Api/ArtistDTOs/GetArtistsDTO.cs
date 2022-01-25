using TL.Domain;

namespace TL.Api.ArtistDTOs;

public class GetArtistsDTO
{
    public List<GetArtistDTO> Artists = new List<GetArtistDTO>();

    public static List<GetArtistDTO> GetAll(IEnumerable<Artist> artists)
    {
        var dto = new GetArtistsDTO();
        foreach (var artist in artists)
        {
            var artistDto = GetArtistDTO.FromArtist(artist);
            dto.Artists.Add(artistDto);
        }

        return dto.Artists;
    }
}