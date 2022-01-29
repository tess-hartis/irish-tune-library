using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TL.Data;
using TL.Domain;
using TL.Repository;

namespace TL.Tests.PgsqlTests;

[TestFixture]
public class AlbumArtistServiceTest
{
    [Test]
    public async Task Can_Get_Artist_Albums()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var albumRepo = new AlbumRepository(context);
        var artistRepo = new ArtistRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var service = new AlbumArtistService(context, albumRepo, artistRepo);
        var artist = Artist.CreateArtist("its the name");
        await artistRepo.AddArtist(artist);
        var album = Album.CreateAlbum("its the title", 2001);
        await albumRepo.AddAlbum(album);
        await service.AddExistingArtistToAlbum(album.Id, artist.Id);
        await context.SaveChangesAsync();

        //Act
        var returned = await service.FindArtistAlbums(artist.Id);

        //Assert
        var expected = 1;
        var actual = returned.Count();
        Assert.AreEqual(expected, actual);
        
    }

    [Test]
    public async Task Can_Add_Existing_Artist_To_Album()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var albumRepo = new AlbumRepository(context);
        var artistRepo = new ArtistRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var service = new AlbumArtistService(context, albumRepo, artistRepo);
        var artist = Artist.CreateArtist("its the name");
        await artistRepo.AddArtist(artist);
        var album = Album.CreateAlbum("its the title", 2001);
        await albumRepo.AddAlbum(album);

        //Act
        await service.AddExistingArtistToAlbum(album.Id, artist.Id);

        //Assert
        const bool expected = true;
        var actual = album.Artists.Contains(artist);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Add_New_Artist_To_Album()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var albumRepo = new AlbumRepository(context);
        var artistRepo = new ArtistRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var service = new AlbumArtistService(context, albumRepo, artistRepo);
        var album = Album.CreateAlbum("its the title", 2001);
        await albumRepo.AddAlbum(album);

        //Act
        await service.AddNewArtistToAlbum(album.Id, "new artist name");

        //Assert
        const int expected = 1;
        var actual = album.Artists.Count;
        Assert.AreEqual(expected, actual);
    }
    
    // [Test]
    // public async Task Can_Remove_Artist_From_Album()
    // {
    //     //Arrange
    //     await using var context = new TuneLibraryContext();
    //     var albumRepo = new AlbumRepository(context);
    //     var artistRepo = new ArtistRepository(context);
    //     await context.Database.EnsureDeletedAsync();
    //     await context.Database.EnsureCreatedAsync();
    //     var service = new AlbumArtistService(context, albumRepo, artistRepo);
    //     var album = Album.CreateAlbum("its the title", 2001);
    //     await albumRepo.AddAlbum(album);
    //     var artist = Artist.CreateArtist("its the name");
    //     await artistRepo.AddArtist(artist);
    //     await service.AddExistingArtistToAlbum(album.Id, artist.Id);
    //     
    //     //Act
    //     await service.RemoveArtistFromAlbum(album.Id, artist.Id);
    //     
    //     const int expected = 0;
    //     var actual = album.Artists.Count;
    //     Assert.AreEqual(expected, actual);
    // }
}