using TL.Domain;

namespace TL.Api.AlbumDTOS;

public class PutAlbumDTO
{
    public string Title { get; set; }
    public int Year { get; set; }

    public static Album UpdatedAlbum(Album album, PutAlbumDTO dto)
    {
        album.Title = dto.Title;
        album.Title = dto.Title;
        return album;
    }
}