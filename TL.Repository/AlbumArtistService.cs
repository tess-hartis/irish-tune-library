using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;

namespace TL.Repository;

public interface IAlbumArtistService
{
    Task<IEnumerable<Album>> FindArtistAlbums(Artist artist);
    Task<IEnumerable<Artist>> FindAlbumArtists(Album album);
    Task<Option<Unit>> AddExistingArtistToAlbum(int albumId, int artistId);
    Task<Option<bool>> RemoveArtistFromAlbum(int albumId, int artistId);
    
}

public class AlbumArtistService : IAlbumArtistService
{
    private readonly TuneLibraryContext _context;
    private IAlbumRepository _albumRepository;
    private IArtistRepository _artistRepository;

    public AlbumArtistService(TuneLibraryContext context)
    {
        _context = context;
    }

    private IAlbumRepository AlbumRepo
    {
        get
        {
            return _albumRepository = new AlbumRepository(_context);
        }
    }

    private IArtistRepository ArtistRepo
    {
        get
        {
            return _artistRepository = new ArtistRepository(_context);
        }
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Album>> FindArtistAlbums(Artist artist)
    {
        var albums = await AlbumRepo
            .GetByWhere(a => a.Artists.Contains(artist)).ToListAsync();
        return albums;
    }

    public async Task<IEnumerable<Artist>> FindAlbumArtists(Album album)
    {
        var artists = await ArtistRepo
            .GetByWhere(a => a.Albums.Contains(album)).ToListAsync();
        return artists;
    }

    public async Task<Option<Unit>> AddExistingArtistToAlbum(int albumId, int artistId)
    {
        var album = await AlbumRepo.FindAsync(albumId);
        var artist = await ArtistRepo.FindAsync(artistId);

        var result = 
            from al in album 
            from ar in artist 
            select al.AddArtist(ar);

        ignore(result.Map(async _ => await _context.SaveChangesAsync()));

        return result;
    }

    public async Task<Option<bool>> RemoveArtistFromAlbum(int albumId, int artistId)
    {
        var album = await AlbumRepo.FindAsync(albumId);
        var artist = await ArtistRepo.FindAsync(artistId);
    
        var result = artist.Bind(a => album.Map(x => x.RemoveArtist(a)));
    
        ignore(result.Map(async _ => await _context.SaveChangesAsync()));
    
        return result;
    }
}