using TL.Domain.ValueObjects.AlbumValueObjects;

namespace TL.Api.DTOs.AlbumDTOs;

public class PutAlbumDTO
{
    public string Title { get; set; }
    public int Year { get; set; }
}