using TL.Domain;
using TL.Domain.ValueObjects.AlbumValueObjects;

namespace TL.Api.DTOs.AlbumDTOs;

public class PutAlbumDTO
{
    public string Title { get; set; }
    public int Year { get; set; }

    public static Album UpdatedAlbum(Album album, PutAlbumDTO dto)
    {
        var title = AlbumTitle.Create(dto.Title);
        var year = AlbumYear.Create(dto.Year);
        album.Update(title, year);
        return album;
    }
}