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
    public async Task Can_Add_Track_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new TrackRepository(context);
        var track = Track.CreateTrack("Kerry Nights", 2);
        
        //Act
        await repo.AddTrack(track);

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
        var track = Track.CreateTrack("Kerry Nights", 2);
        await repo.AddTrack(track);
        
        //Act
        await repo.DeleteTrack(track.Id);
        var list = await repo.GetEntities().ToListAsync();
        
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
        var repo = new TrackRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var track = Track.CreateTrack("Kerry Nights", 2);
        await repo.AddTrack(track);

        //Act
        await repo.UpdateTrackTitle(track.Id, "New Title");

        const string expected = "New Title";
        var actual = track.Title;

        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Update_TrackNumber_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TrackRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var track = Track.CreateTrack("Kerry Nights", 2);
        await repo.AddTrack(track);

        //Act
        await repo.UpdateTrackNumber(track.Id, 3);

        const int expected = 3;
        var actual = track.TrackNumber;

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
        var track = Track.CreateTrack("Kerry Nights", 2);
        await repo.AddAsync(track);

        //Act
        const string expected = "Kerry Nights";
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
        var track = Track.CreateTrack("Kerry Nights", 2);
        await repo.AddAsync(track);
        
        //Act
        const int expected = 1;
        var actual = await repo.FindByExactTitle("Kerry Nights");

        //Assert
        Assert.AreEqual(expected, actual.Count());
    }
    
}