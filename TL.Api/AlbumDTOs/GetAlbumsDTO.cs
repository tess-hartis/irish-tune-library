using TL.Api.TrackDTOs;
using TL.Domain;

namespace TL.Api.AlbumDTOs;

public class GetAlbumsDTO
{
    private List<GetAlbumDTO> _albums { get; } = new List<GetAlbumDTO>();
    public IEnumerable<GetAlbumDTO> AllAlbums => _albums;

    public static GetAlbumsDTO GetAll(IEnumerable<Album> albums)
    {
        var dto = new GetAlbumsDTO();
        foreach (var album in albums)
        {
            var albumDto = GetAlbumDTO.FromAlbum(album);
            dto._albums.Add(albumDto);
        }

        return dto;
    }
}