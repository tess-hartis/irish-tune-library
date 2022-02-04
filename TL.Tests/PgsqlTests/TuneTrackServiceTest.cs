using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TL.Data;
using TL.Domain;
using TL.Repository;

namespace TL.Tests.PgsqlTests;

[TestFixture]
public class TuneTrackServiceTest
{
    [Test]
    public async Task Can_Get_Tune_Recordings()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var trackRepo = new TrackRepository(context);
        var tuneRepo = new TuneRepository(context);
        var albumRepo = new AlbumRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var service = new TuneTrackService(context, tuneRepo, trackRepo);
        var album = Album.CreateAlbum("album title", 2000);
        await albumRepo.AddAsync(album);
        var tune = Tune.CreateTune("tune title", "composerrr", "Jig", "DMaj");
        await tuneRepo.AddAsync(tune);
        var track = Track.CreateTrack("track title", 3);
        track.AlbumId = album.Id;
        await trackRepo.AddAsync(track);
        await service.AddExistingTuneToTrack(track.Id, tune.Id);
        await context.SaveChangesAsync();

        //Act
        var returned = await service.FindTracksByTune(tune.Id);

        //Assert
        var expected = 1;
        var actual = returned.Count();
        Assert.AreEqual(expected, actual);
        
    }
}