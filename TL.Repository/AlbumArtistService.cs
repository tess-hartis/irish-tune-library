using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.ValueObjects.ArtistValueObjects;

namespace TL.Repository;

public interface IAlbumArtistService
{
    Task<IEnumerable<Album>> FindArtistAlbums(int artistId);
    Task<IEnumerable<Artist>> FindAlbumArtists(int albumId);
    Task<Album> AddExistingArtistToAlbum(int albumId, int artistId);
    Task<Album> AddNewArtistToAlbum(int albumId, Artist artist);
    Task<Album> RemoveArtistFromAlbum(int albumId, int artistId);
    
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
    
    public async Task<IEnumerable<Album>> FindArtistAlbums(int artistId)
    {
        var artist = await _artistRepository.FindAsync(artistId);
        var albums = await _albumRepository
            .GetByWhere(a => a.Artists.Contains(artist)).ToListAsync();
        return albums;
    }

    public async Task<IEnumerable<Artist>> FindAlbumArtists(int albumId)
    {
        var album = await _albumRepository.FindAsync(albumId);
        var artists = await _artistRepository
            .GetByWhere(a => a.Albums.Contains(album)).ToListAsync();
        return artists;
    }
    
    public async Task<Album> AddExistingArtistToAlbum(int albumId, int artistId)
    {
        var album = await _albumRepository.FindAsync(albumId);

        if (album == null)
            throw new EntityNotFoundException($"Album with ID '{albumId}' was not found");
        
        var artist = await _artistRepository.FindAsync(artistId);
        album.AddArtist(artist);
        await SaveChangesAsync();
        return album;
    }

    public async Task<Album> AddNewArtistToAlbum(int albumId, Artist artist)
    {
        var album = await _albumRepository.FindAsync(albumId);
        await _artistRepository.AddAsync(artist);
        album.AddArtist(artist);
        await SaveChangesAsync();
        return album;
    }

    public async Task<Album> RemoveArtistFromAlbum(int albumId, int artistId)
    {
        var album = await _context.Albums
            .Include(a => a.Artists)
            .FirstOrDefaultAsync(a => a.Id == albumId);
        
        if (album == null)
            throw new EntityNotFoundException($"Album with ID '{albumId}' was not found");
        
        var artist = await _artistRepository.FindAsync(artistId);
        album.RemoveArtist(artist);
        await SaveChangesAsync();
        return album;
    }
}