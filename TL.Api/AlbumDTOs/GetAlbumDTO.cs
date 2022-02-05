using TL.Api.TrackDTOs;
using TL.Domain;

namespace TL.Api.AlbumDTOs;

public class GetAlbumDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }

    public static GetAlbumDTO FromAlbum(Album album)
    {
        return new GetAlbumDTO
        {
            Id = album.Id,
            Title = album.Title.Value,
            Year = album.Year.Value
        };
    }
}