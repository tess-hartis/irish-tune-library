// using NUnit.Framework;
//
// namespace TL.Tests.PgsqlTests;
//
// [TestFixture]
// public class UnitOfWorkTest
// {
//     [Test]
//     public async Task Can_Find_By_Tune_Featured()
//     {
//         //Arrange
//         await using var context = new TuneLibraryContext();
//         await context.Database.EnsureDeletedAsync();
//         await context.Database.EnsureCreatedAsync();
//         var repo = new TrackRepository(context);
//         var track = Track.CreateTrack("Title", 1);
//         await repo.AddTrack(track);
//         var tune = Tune.CreateTune("Kesh", "Traditional", TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
//         context.Tunes.Add(tune);
//         track.AddExistingTune(tune);
//         await repo.SaveAsync();
//         
//         //Act
//         const bool expected = true;
//         var actual = track.TuneList.Contains(tune);
//
//         //Assert
//         Assert.AreEqual(expected, actual);
//
//     }
// }