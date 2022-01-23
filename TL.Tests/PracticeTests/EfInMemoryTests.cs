// using Microsoft.EntityFrameworkCore;
// using NUnit.Framework;
// using TL.Data;
// using TL.Domain;
//
// namespace TL.Tests.PracticeTests;
//
// [TestFixture]
// public class EfInMemoryTests
// {
//     [Test]
//     public void Can_Insert_Tune_Into_Database()
//     {
//         
//         var builder = new DbContextOptionsBuilder(); //creates options builder using EF
//        
//         //you can use different InMemory databases for each test,
//         //or you can use the same instance of the database for related tests
//         builder.UseInMemoryDatabase("CanInsertTune");  
//         
//         using (var context = new TuneLibraryContext(builder.Options)) 
//         {
//             var tune = new Tune("Kesh", TuneTypeEnum.Jig, 
//                 TuneKeyEnum.GMaj, "Traditional");
//             context.Tunes.Add(tune);
//             // context.SaveChanges(); // is not actually using context. InMemory only uses lists
//             
//             // Assert.AreNotEqual(0, tune.Id); //this is irrelevant to use when dealing with InMemory because
//             // when ".Add" is used, a new tune is added to the InMemory list and does not use "Save Changes.'
//             // As soon as ".Add" is used, the id of the tune is already set.
//             // alternate option: check that the count of InMemory = 1.
//             //alternate option: 
//             
//             Assert.AreEqual(EntityState.Added,
//                 context.Entry(tune).State);
//         }
//     }
// }