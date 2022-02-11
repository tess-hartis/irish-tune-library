using TL.Domain;
using TL.Domain.ValueObjects.AlbumValueObjects;

namespace TL.Api.DTOs.AlbumDTOs;

public class PostAlbumDTO
{
    public string Title { get; set; }
    public int Year { get; set; }

    public static Album ToAlbum(PostAlbumDTO dto)
    {
        var title = AlbumTitle.Create(dto.Title);
        var year = AlbumYear.Create(dto.Year);
        var album = Album.Create(title, year);
        return album;
    }
}