using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TL.Data;
using TL.Domain;
using TL.Repository;

namespace TL.Tests.PgsqlTests;

[TestFixture]
public class TrackRepoTest
{
    [Test]
    public void Can_Add_Track_Using_Context_Directly()
    {
        using var context = new TuneLibraryContext(); // need to extract these three lines
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var track = new Track("Title", 1);

        context.Tracks.Add(track);
        context.SaveChanges();

        const int expected = 0;
        var actual = track.Id;
        
        Assert.AreNotEqual(expected, actual);
    }

    [Test]
    public async Task Can_Add_Track_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new TrackRepository(context);
        var track = new Track("Title", 1);
        
        //Act
        await repo.Add(track);

        const int expected = 1;
        var actual = track.Id;
        
        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Delete_Track_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new TrackRepository(context);
        var track = new Track("Title", 1);
        await repo.AddAsync(track);
        
        //Act
        await repo.Delete(track.Id);
        var list = await repo.GetAll().ToListAsync();
        const int expected = 0;
        var actual = list.Count;

        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Update_Track_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TrackRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var track = new Track("Title", 1);
        await repo.AddAsync(track);

        //Act
        track.Title = "New Title";
        await repo.Update(track.Id);

        const string expected = "New Title";
        var actual = track.Title;

        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Find_Track_By_Id()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TrackRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var track = new Track("Title", 1);
        await repo.AddAsync(track);

        //Act
        const string expected = "Title";
        var actual = await repo.FindAsync(track.Id);

        //Assert
        Assert.AreEqual(expected, actual.Title);
    }
    
    [Test]
    public async Task Can_Find_All_Tracks_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TrackRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        //Act
        var tracks = await repo.GetAllTracks();
        const int expected = 0;
        var actual = tracks.Count();

        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Find_By_Exact_Title_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new TrackRepository(context);
        var track = new Track("Title", 1);
        await repo.AddAsync(track);
        
        //Act
        const string expected = "Title";
        var actual = await repo.FindByExactTitleAsync("Title");

        //Assert
        Assert.AreEqual(expected, actual.Title);
    }

    [Test]
    public async Task Can_Find_By_Tune_Featured()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new TrackRepository(context);
        var track = new Track("Title", 1);
        await repo.AddAsync(track);
        var tune = new Tune("Misty", TuneTypeEnum.Air, TuneKeyEnum.Ador, "Traditional");
        track.AddTune(tune);
        await repo.SaveAsync();
        
        //Act
        const int expected = 1;
        var actual = track.TuneList.Count;
        
        //Assert
        Assert.AreEqual(expected, actual);

    }
}