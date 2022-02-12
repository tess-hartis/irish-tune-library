using TL.Api.DTOs.AlbumDTOs;
using TL.Domain;

namespace TL.Api.DTOs.ArtistDTOs;

public class GetArtistDTO
{
    public string Name { get; set; }
    public int Id { get; set; }
    public IEnumerable<GetAlbumDTO> Albums { get; set; } = new List<GetAlbumDTO>();

    public static GetArtistDTO FromArtist(Artist artist)
    {
        // var albums = from a in artist.Albums
        //     select GetAlbumDTO.FromAlbum(a);

        var albums = artist.Albums.Select(GetAlbumDTO.FromAlbum);
        
        return new GetArtistDTO
        {
            Name = artist.Name.Value,
            Id = artist.Id,
            Albums = albums
        };
    }
}