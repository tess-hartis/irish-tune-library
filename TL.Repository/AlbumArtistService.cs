using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface IAlbumArtistService
{
    Task<IEnumerable<Album>> FindArtistAlbums(int artistId);
    Task SaveChangesAsync();
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
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Album>> FindArtistAlbums(int artistId)
    {
        var artist = await _artistRepository.FindAsync(artistId);
        var albums = await _albumRepository.FindByArtist(artist);
        return albums;
    }
}