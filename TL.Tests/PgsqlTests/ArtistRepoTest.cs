using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TL.Data;
using TL.Domain;
using TL.Repository;

namespace TL.Tests.PgsqlTests;

[TestFixture]
public class ArtistRepoTest
{
    [Test]
    public void Can_Add_Artist_Using_Context_Directly()
    {
        using var context = new TuneLibraryContext(); // need to extract these three lines
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var artist = new Artist("Liz Carroll");

        context.Artists.Add(artist);
        context.SaveChanges();

        const int expected = 0;
        var actual = artist.Id;
        
        Assert.AreNotEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Add_Artist_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new ArtistRepository(context);
        var artist = new Artist("Liz Carroll");
        
        //Act
        await repo.Add(artist);

        const int expected = 1;
        var actual = artist.Id;
        
        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Delete_Artist_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new ArtistRepository(context);
        var artist = new Artist("Name");
        await repo.Add(artist);
        
        //Act
        await repo.Delete(artist.Id);
        var list = await repo.GetAllArtists();
        const int expected = 0;
        var actual = list.Count();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task Can_Update_Artist_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new ArtistRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var artist = new Artist("Liz Carroll");
        await repo.Add(artist);

        //Act
        artist.Name = "Liz Carroll";
        await repo.Update(artist.Id);

        const string expected = "Liz Carroll";
        var actual = artist.Name;

        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Find_Artist_By_Id()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new ArtistRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var artist = new Artist("Name");
        await repo.Add(artist);

        //Act
        const string expected = "Name";
        var actual = await repo.Find(artist.Id);

        //Assert
        Assert.AreEqual(expected, actual.Name);
    }
    
    [Test]
    public async Task Can_Find_All_Artists_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new ArtistRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        //Act
        var artists = await repo.GetAllArtists();
        const int expected = 0;
        var actual = artists.Count();

        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Find_By_Exact_Name_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new ArtistRepository(context);
        var artist = new Artist("Name");
        await repo.Add(artist);
        
        //Act
        const int expected = 1;
        var actual = await repo.GetByName("Name");

        //Assert
        Assert.AreEqual(expected, actual.Count());
    }
}