using TL.Domain;

namespace TL.Api.AlbumDTOs;

public class PutAlbumDTO
{
    public string Title { get; set; }
    public int Year { get; set; }

    public static Album UpdatedAlbum(Album album, PutAlbumDTO dto)
    {
        album.UpdateTitle(dto.Title);
        album.UpdateYear(dto.Year);
        return album;
    }
}