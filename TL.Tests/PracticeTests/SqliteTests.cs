using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TL.Domain;
using TL.Repository;

namespace TL.Tests.PracticeTests;

[TestFixture]
public class SqliteTests : SqliteTestBase
{
    public SqliteTests()
    {
        UseSqlite();
    }
   // [Test]
    public async Task Can_Get_All_Tunes()
    {
        await using var context = SqLiteConnection(); // desperately need to extract these lines
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync(); 
        await context.Database.EnsureCreatedAsync();
        await context.Database.OpenConnectionAsync();
        await context.SaveChangesAsync();
        var tunes = await repo.GetAllTunes();
        Assert.AreEqual(tunes.Count(), 0);

    }

    //[Test]
    public async Task Can_Add_Tunes()
    {
        await using var context = SqLiteConnection();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        await context.Database.OpenConnectionAsync();
        var tune = new Tune("Kesh", TuneTypeEnum.Jig, TuneKeyEnum.GMaj,
            "Traditional");
        await repo.Add(tune);
        var tunes = await repo.GetAllTunes();
        Assert.AreEqual(tunes.Count(), 1);
    }
    
   // [Test]
    public async Task Can_Delete_Tunes()
    {
        await using var context = SqLiteConnection();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        await context.Database.OpenConnectionAsync();
        var tune = new Tune("Kesh", TuneTypeEnum.Jig, TuneKeyEnum.GMaj,
            "Traditional");
        await repo.Add(tune);
        await repo.Delete(tune.Id);
        var tunes = await repo.GetAllTunes();
        Assert.AreEqual(tunes.Count(), 0);
    }

   // [Test]
    public async Task Can_Find_Tune_By_Id()
    {
        await using var context = SqLiteConnection();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        await context.Database.OpenConnectionAsync();
        var tune1 = new Tune("Kesh", TuneTypeEnum.Jig, TuneKeyEnum.GMaj,
            "Traditional");
        await repo.Add(tune1);
        var tune2 = new Tune("Rolling Waves", TuneTypeEnum.Jig, TuneKeyEnum.GMaj,
            "Traditional");
        await repo.Add(tune2);

        var expected = await repo.FindAsync(1);
        var actual = "Kesh";

        Assert.AreEqual(expected.Title, actual);
    }

   // [Test]
    public async Task Can_Update_Tune()
    {
        await using var context = SqLiteConnection();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        await context.Database.OpenConnectionAsync();
        var tune1 = new Tune("Kesh", TuneTypeEnum.Jig, TuneKeyEnum.GMaj,
            "Traditional");
        await repo.Add(tune1);
        
        tune1.Title = "New Title";
        await repo.Update(tune1.Id);

        var expected = "New Title";
        var actual = tune1.Title;
        
        Assert.AreEqual(expected, actual);
        
        
    }
}