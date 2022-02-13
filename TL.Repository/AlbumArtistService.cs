using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.ValueObjects.ArtistValueObjects;

namespace TL.Repository;

public interface IAlbumArtistService
{
    Task<IEnumerable<Album>> FindArtistAlbums(Artist artist);
    Task<IEnumerable<Artist>> FindAlbumArtists(Album album);
    Task<Album> AddExistingArtistToAlbum(Album album, Artist artist);
    Task<Album> AddNewArtistToAlbum(Album album, Artist artist);
    Task<Album> RemoveArtistFromAlbum(Album album, Artist artist);
    
}

public class AlbumArtistService : IAlbumArtistService
{
    private readonly TuneLibraryContext _context;
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;

    public AlbumArtistService(TuneLibraryContext context, IAlbumRepository albumRepository, IArtistRepository artistRepository)
    {
        _context = context;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Album>> FindArtistAlbums(Artist artist)
    {
        var albums = await _albumRepository
            .GetByWhere(a => a.Artists.Contains(artist)).ToListAsync();
        return albums;
    }

    public async Task<IEnumerable<Artist>> FindAlbumArtists(Album album)
    {
        var artists = await _artistRepository
            .GetByWhere(a => a.Albums.Contains(album)).ToListAsync();
        return artists;
    }
    
    public async Task<Album> AddExistingArtistToAlbum(Album album, Artist artist)
    {
        album.AddArtist(artist);
        await SaveChangesAsync();
        return album;
    }

    public async Task<Album> AddNewArtistToAlbum(Album album, Artist artist)
    {
        await _artistRepository.AddAsync(artist);
        album.AddArtist(artist);
        await SaveChangesAsync();
        return album;
    }

    public async Task<Album> RemoveArtistFromAlbum(Album album, Artist artist)
    {
        album.RemoveArtist(artist);
        await SaveChangesAsync();
        return album;
    }
}