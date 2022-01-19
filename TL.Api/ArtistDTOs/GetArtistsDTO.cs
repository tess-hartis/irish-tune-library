using TL.Domain;

namespace TL.Api.ArtistDTOs;

public class GetArtistsDTO
{
    private List<GetArtistDTO> _artists { get; } = new List<GetArtistDTO>();
    public IEnumerable<GetArtistDTO> AllArtists => _artists;

    public static GetArtistsDTO GetAll(IEnumerable<Artist> artists)
    {
        var dto = new GetArtistsDTO();
        foreach (var artist in artists)
        {
            var artistDto = GetArtistDTO.FromArtist(artist);
            dto._artists.Add(artistDto);
        }

        return dto;
    }
}