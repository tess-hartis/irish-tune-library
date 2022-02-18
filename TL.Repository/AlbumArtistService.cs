// using LanguageExt;
// using static LanguageExt.Prelude;
// using Microsoft.EntityFrameworkCore;
// using TL.Data;
// using TL.Domain;
// using TL.Domain.Exceptions;
// using TL.Domain.ValueObjects.ArtistValueObjects;
//
// namespace TL.Repository;
//
// public interface IAlbumArtistService
// {
//     Task<IEnumerable<Album>> FindArtistAlbums(Artist artist);
//     Task<IEnumerable<Artist>> FindAlbumArtists(Album album);
//     Task<Option<Unit>> AddExistingArtistToAlbum(int albumId, int artistId);
//     // Task<Album> AddNewArtistToAlbum(Album album, Artist artist);
//     // Task<Option<Album>> RemoveArtistFromAlbum(int albumId, int artistId);
//     
// }
//
// public class AlbumArtistService : IAlbumArtistService
// {
//     private readonly TuneLibraryContext _context;
//     private readonly IAlbumRepository _albumRepository;
//     private readonly IArtistRepository _artistRepository;
//
//     public AlbumArtistService(TuneLibraryContext context, IAlbumRepository albumRepository, IArtistRepository artistRepository)
//     {
//         _context = context;
//         _albumRepository = albumRepository;
//         _artistRepository = artistRepository;
//     }
//
//     private async Task SaveChangesAsync()
//     {
//         await _context.SaveChangesAsync();
//     }
//     
//     public async Task<IEnumerable<Album>> FindArtistAlbums(Artist artist)
//     {
//         var albums = await _albumRepository
//             .GetByWhere(a => a.Artists.Contains(artist)).ToListAsync();
//         return albums;
//     }
//
//     public async Task<IEnumerable<Artist>> FindAlbumArtists(Album album)
//     {
//         var artists = await _artistRepository
//             .GetByWhere(a => a.Albums.Contains(album)).ToListAsync();
//         return artists;
//     }
//
//     public async Task<Option<Unit>> AddExistingArtistToAlbum(int albumId, int artistId)
//     {
//         var album = await _albumRepository.FindAsync(albumId);
//         var artist = await _artistRepository.FindAsync(artistId);
//
//         var result = 
//             from al in album 
//             from ar in artist 
//             select al.AddArtist(ar);
//
//         var result1 = artist.Bind(a => album.Map(x => x.AddArtist(a)));
//
//         ignore(result.Map(async _ => await _context.SaveChangesAsync()));
//
//         return result;
//     }
//
//     // public async Task<Option<Album>> RemoveArtistFromAlbum(int albumId, int artistId)
//     // {
//     //     var album = await _albumRepository.FindAsync(albumId);
//     //     var artist = await _artistRepository.FindAsync(artistId);
//     //
//     //     var result = artist.Bind(a => album.Map(x => x.RemoveArtist(a)));
//     //
//     //     ignore(result.Map(async _ => await _context.SaveChangesAsync()));
//     //
//     //     return result;
//     // }
// }