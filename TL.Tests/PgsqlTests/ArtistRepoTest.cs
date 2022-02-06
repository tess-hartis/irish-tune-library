// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using NUnit.Framework;
// using TL.Data;
// using TL.Domain;
// using TL.Repository;
//
// namespace TL.Tests.PgsqlTests;
//
// [TestFixture]
// public class ArtistRepoTest
// {
//     [Test]
//     public async Task Can_Add_Artist_Using_Repository()
//     {
//         //Arrange
//         await using var context = new TuneLibraryContext();
//         await context.Database.EnsureDeletedAsync();
//         await context.Database.EnsureCreatedAsync();
//         var repo = new ArtistRepository(context);
//         var artist = Artist.CreateArtist("Liz Carroll");
//         
//         //Act
//         await repo.AddAsync(artist);
//
//         const int expected = 1;
//         var actual = artist.Id;
//         
//         //Assert
//         Assert.AreEqual(expected, actual);
//     }
//     
//     [Test]
//     public async Task Can_Delete_Artist_Using_Repository()
//     {
//         //Arrange
//         await using var context = new TuneLibraryContext();
//         await context.Database.EnsureDeletedAsync();
//         await context.Database.EnsureCreatedAsync();
//         var repo = new ArtistRepository(context);
//         var artist = Artist.CreateArtist("Liz Carroll");
//         await repo.AddAsync(artist);
//         
//         //Act
//         await repo.DeleteAsync(artist.Id);
//         var list = await repo.GetEntities().ToListAsync();
//         const int expected = 0;
//         var actual = list.Count;
//
//         //Assert
//         Assert.AreEqual(expected, actual);
//     }
//
//     [Test]
//     public async Task Can_Find_Artist_By_Id()
//     {
//         //Arrange
//         await using var context = new TuneLibraryContext();
//         var repo = new ArtistRepository(context);
//         await context.Database.EnsureDeletedAsync();
//         await context.Database.EnsureCreatedAsync();
//         var artist = Artist.CreateArtist("Liz Carroll");
//         await repo.AddAsync(artist);
//
//         //Act
//         const string expected = "Liz Carroll";
//         var actual = await repo.FindAsync(artist.Id);
//         //Assert
//         Assert.AreEqual(expected, actual.Name);
//     }
//     
//     [Test]
//     public async Task Can_Find_All_Artists_Using_Repository()
//     {
//         //Arrange
//         await using var context = new TuneLibraryContext();
//         var repo = new ArtistRepository(context);
//         await context.Database.EnsureDeletedAsync();
//         await context.Database.EnsureCreatedAsync();
//
//         //Act
//         var artists = await repo.GetEntities().ToListAsync();
//         const int expected = 0;
//         var actual = artists.Count;
//
//         //Assert
//         Assert.AreEqual(expected, actual);
//     }
//     
// }