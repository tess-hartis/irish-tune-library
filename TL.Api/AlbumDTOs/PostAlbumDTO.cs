using TL.Domain;
using TL.Domain.ValueObjects.AlbumValueObjects;

namespace TL.Api.AlbumDTOs;

public class PostAlbumDTO
{
    public string Title { get; set; }
    public int Year { get; set; }

    public static Album ToAlbum(PostAlbumDTO dto)
    {
        var title = AlbumTitle.Create(dto.Title);
        var album = Album.CreateAlbum(title, dto.Year);
        return album;
    }
}