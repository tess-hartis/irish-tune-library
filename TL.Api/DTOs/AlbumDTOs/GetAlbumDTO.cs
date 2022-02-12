using TL.Domain;

namespace TL.Api.DTOs.AlbumDTOs;

public class GetAlbumDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public IEnumerable<string> Artists { get; set; }

    public static GetAlbumDTO FromAlbum(Album album)
    {
        var artistNames = album.Artists.Select(x => x.Name.Value);
        
        return new GetAlbumDTO
        {
            Id = album.Id,
            Title = album.Title.Value,
            Year = album.Year.Value,
            Artists = artistNames
        };
    }
}