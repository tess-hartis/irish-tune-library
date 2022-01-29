using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TL.Data;
using TL.Domain;
using TL.Repository;

namespace TL.Tests.PgsqlTests;

[TestFixture]
public class AlbumRepoTest
{
    [Test]
    public async Task Can_Add_Album_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new AlbumRepository(context);
        var album = Album.CreateAlbum("Making Time", 2000);
        
        //Act
        await repo.AddAlbum(album);

        const int expected = 1;
        var actual = album.Id;
        
        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Delete_Album_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new AlbumRepository(context);
        var album = Album.CreateAlbum("Making Time", 2000);
        await repo.AddAlbum(album);
        
        //Act
        await repo.DeleteAlbum(album.Id);
        var list =  await repo.GetEntities().ToListAsync();
        const int expected = 0;
        var actual = list.Count;

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task Can_Update_Title_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new AlbumRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var album = Album.CreateAlbum("Making Time", 2000);
        await repo.AddAlbum(album);

        //Act
        await repo.UpdateAlbum(album.Id, "New Title",2003);
        

        //Assert
        Assert.AreEqual(album.Title, "New Title");
        Assert.AreEqual(album.Year, 2003);
    }

    [Test]
    public async Task Can_Find_Album_By_Id()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new AlbumRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var album = Album.CreateAlbum("Making Time", 2000);
        await repo.AddAlbum(album);

        //Act
        const string expected = "Making Time";
        var actual = await repo.FindAsync(album.Id);

        //Assert
        Assert.AreEqual(expected, actual.Title);
    }

    [Test] 
    public async Task Can_Find_All_Albums_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new AlbumRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        //Act
       
        const int expected = 0;
        var actual = await repo.GetAllAlbums();

        //Assert
        Assert.AreEqual(expected, actual.Count());
    }
    
}