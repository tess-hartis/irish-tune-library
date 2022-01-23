using TL.Api.TrackDTOs;
using TL.Domain;

namespace TL.Api.AlbumDTOs;

public class GetAlbumDTO
{
    public string Title { get; set; }
    public int Year { get; set; }

    public static GetAlbumDTO FromAlbum(Album album)
    {
        return new GetAlbumDTO
        {
            Title = album.Title,
            Year = album.Year
        };
    }
}