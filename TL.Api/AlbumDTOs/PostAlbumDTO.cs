using TL.Domain;

namespace TL.Api.AlbumDTOs;

public class PostAlbumDTO
{
    public string Title { get; set; }
    public int Year { get; set; }

    public static Album ToAlbum(PostAlbumDTO dto)
    {
        var album = Album.CreateAlbum(dto.Title, dto.Year);
        return album;
    }
}