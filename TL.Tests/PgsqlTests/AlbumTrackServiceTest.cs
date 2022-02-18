using System.Threading.Tasks;
using NUnit.Framework;
using TL.Data;
using TL.Domain;
using TL.Domain.ValueObjects.AlbumValueObjects;
using TL.Domain.ValueObjects.TrackValueObjects;
using TL.Repository;

namespace TL.Tests.PgsqlTests;

[TestFixture]
public class AlbumTrackServiceTest
{
    [Test]
    public async Task Can_Add_Track_To_Album_And_Set_TrackNumber()
    {
        //Arrange
         await using var context = new TuneLibraryContext();
         await context.Database.EnsureDeletedAsync();
         await context.Database.EnsureCreatedAsync();
         var albumRepo = new AlbumRepository(context);
         var trackRepo = new TrackRepository(context);
         var albumTrackRepo = new AlbumTrackService(context, albumRepo, trackRepo);
         var title = AlbumTitle.Create("Making Time");
         var year = AlbumYear.Create(2000);
         var album = Album.Create(title, year);
         await albumRepo.AddAsync(album);
         
         //Act
         var trackTitle = TrackTitle.Create("new Track");
         var trackNumber = TrkNumber.Create(3);
         await albumTrackRepo.AddNewTrackToAlbum(album.Id, trackTitle, trackNumber);
         
         //Assert
         var foundTrack = await trackRepo.FindAsync(1);
         var expected = 3;
         var actual = foundTrack.TrackNumber.Value;
         
         Assert.AreEqual(expected, actual);

    }

}