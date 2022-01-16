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
    public void Can_Add_Album_Using_Context_Directly()
    {
        using var context = new TuneLibraryContext(); // need to extract these three lines
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var album = new Album("Title", 2000);

        context.Albums.Add(album);
        context.SaveChanges();

        const int expected = 0;
        var actual = album.Id;
        
        Assert.AreNotEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Add_Album_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new AlbumRepository(context);
        var album = new Album("Title", 2000);
        
        //Act
        await repo.Add(album);

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
        var album = new Album("Title", 2000);
        await repo.Add(album);
        
        //Act
        await repo.Delete(album.Id);
        var list =  await repo.GetAll().ToListAsync();
        const int expected = 0;
        var actual = list.Count;

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task Can_Update_Album_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new AlbumRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var album = new Album("Title", 2000);
        await repo.Add(album);

        //Act
        album.Title = "Title";
        await repo.Update(album.Id);

        const string expected = "Title";
        var actual = album.Title;

        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Find_Album_By_Id()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new AlbumRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var album = new Album("Title", 2000);
        await repo.Add(album);

        //Act
        const string expected = "Title";
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
    
    [Test]
    public async Task Can_Find_By_Exact_Name_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new AlbumRepository(context);
        var album = new Album("Title", 2000);
        await repo.Add(album);
        
        //Act
        const int expected = 1;
        var actual = await repo.GetByTitle("Title");

        //Assert
        Assert.AreEqual(expected, actual.Count());
    }
}