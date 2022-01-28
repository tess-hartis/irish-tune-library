using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IAlbumArtistService
{
    Task<IEnumerable<Album>> GetAlbumsByArtist(int artistId);
    Task AddExistingArtistToAlbum(int albumId, int artistId);
    Task AddNewArtistToAlbum(int albumId, string name);
    Task RemoveArtistFromAlbum(int albumId, int artistId);
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
    
    public async Task<IEnumerable<Album>> GetAlbumsByArtist(int artistId)
    {
        var artist = await _artistRepository.FindAsync(artistId);
        var albums = await _albumRepository.GetByWhere(a => a.Artists.Contains(artist)).ToListAsync();
        return albums;
    }
    
    public async Task AddExistingArtistToAlbum(int albumId, int artistId)
    {
        var album = await _albumRepository.FindAsync(albumId);
        var artist = await _artistRepository.FindAsync(artistId);
        album.AddArtist(artist);
        await SaveChangesAsync();
    }

    public async Task AddNewArtistToAlbum(int albumId, string name)
    {
        var album = await _albumRepository.FindAsync(albumId);
        var artist = Artist.CreateArtist(name);
        await _artistRepository.AddArtist(artist);
        album.AddArtist(artist);
        await SaveChangesAsync();
    }

    public async Task RemoveArtistFromAlbum(int albumId, int artistId)
    {
        var album = await _albumRepository.FindAsync(albumId);
        album.RemoveArtist(artistId);
        await SaveChangesAsync();
    }
}