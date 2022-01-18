using TL.Domain;

namespace TL.Api.AlbumDTOS;

public class PostAlbumDTO
{
    public string Title { get; set; }
    public int Year { get; set; }

    public static Album ToAlbum(PostAlbumDTO dto)
    {
        return new Album(dto.Title, dto.Year);
    }
}