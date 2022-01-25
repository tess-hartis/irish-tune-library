using TL.Api.TrackDTOs;
using TL.Domain;

namespace TL.Api.AlbumDTOs;

public class GetAlbumsDTO
{
    public List<GetAlbumDTO> Albums = new List<GetAlbumDTO>();
    public static List<GetAlbumDTO> GetAll(IEnumerable<Album> albums)
    {
        var dto = new GetAlbumsDTO();
        foreach (var album in albums)
        {
            var albumDto = GetAlbumDTO.FromAlbum(album);
            dto.Albums.Add(albumDto);
        }

        return dto.Albums;
    }
}