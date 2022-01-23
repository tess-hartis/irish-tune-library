using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TL.Data;
using TL.Domain;
using TL.Repository;

namespace TL.Tests.PgsqlTests;

[TestFixture]
public class TuneRepoTest
{
    //I am using my *actual* database for testing, but you should
    //not actually do this. View the "InMemoryTests" to see the tests using
    //EntityFramework.InMemory. View the "SqliteTuneRepoTest" to see the tune
    // repository tests using Sqlite InMemory (although they are not working currently)
    
    [Test]
    public async Task Can_Add_Tune_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new TuneRepository(context);
        var tune1 = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        var tune2 = Tune.CreateTune("Banshee", "Traditional",
            TuneTypeEnum.Reel, TuneKeyEnum.GMaj);
        
        //Act
        await repo.AddTune(tune1);
        await repo.AddTune(tune2);

        const int expected = 0;
        var actual = tune1.Id;

        //Assert
        Assert.AreNotEqual(expected, actual);
    }

    [Test]
    public async Task Can_Delete_Tune_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new TuneRepository(context);
        var tune = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        await repo.AddTune(tune);

        //Act
        await repo.DeleteTune(tune.Id);
        var tunes = await repo.GetAllTunes();
        const int expected = 0;
        var actual = tunes.Count();
        
        //Assert
        Assert.AreEqual(expected, actual);
        
    }

    [Test]
    public async Task Can_Update_Title_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        await repo.AddTune(tune);

        //Act
        await repo.UpdateTuneTitle(tune.Id, "New Title");

        const string expected = "New Title";
        var actual = tune.Title;

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task Can_Find_Tune_By_Id_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune1 = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        await repo.AddTune(tune1);
        var tune2 = Tune.CreateTune("Rolling Waves", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.DMaj);
        await repo.AddTune(tune2);

        //Act
        const string expected = "Kesh";
        var actual= await repo.FindAsync(1);
        
        //Assert
        Assert.AreEqual(expected, actual.Title);
    }

    [Test]
    public async Task Can_Find_All_Tunes_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        //Act
        var tunes = await repo.GetAllTunes();
        const int expected = 0;
        var actual = tunes.Count();
       
        //Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task Can_Add_Alternate_Titles_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        await repo.AddTune(tune);
        
        //Act
        await repo.AddAlternateTitle(tune.Id, "Extra Title");

        const string expected = "Extra Title";
        var actual = tune.AlternateTitles[0];
        
        //Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task Sets_Date_Correctly()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        
        //Act
        await repo.AddTune(tune);
        var expected = new DateOnly(2022, 01, 22);
        var actual = tune.DateAdded;
        
        //Assert
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public async Task Can_Find_All_Tunes_By_TuneType_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune1 = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        await repo.AddTune(tune1);
        var tune2 = Tune.CreateTune("Rolling Waves", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.DMaj);
        await repo.AddTune(tune2);
        
        //Act
        
        const int expected = 2;
        var actual = await repo.FindByType(TuneTypeEnum.Jig);
        
        //Assert
        Assert.AreEqual(expected, actual.Count());
    }

    [Test]
    public async Task Can_Find_Tunes_By_TuneKey_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune1 = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        await repo.AddTune(tune1);
        var tune2 = Tune.CreateTune("Rolling Waves", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.DMaj);
        await repo.AddTune(tune2);
        
        //Act
        const int expected = 1;
        var actual = await repo.FindByKey(TuneKeyEnum.DMaj);
        
        //Assert
        Assert.AreEqual(expected, actual.Count());
    }

    [Test]
    public async Task Can_Find_Tunes_By_TuneType_And_TuneKey_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune1 = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        await repo.AddTune(tune1);
        var tune2 = Tune.CreateTune("Rolling Waves", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.DMaj);
        await repo.AddTune(tune2);

        //Act
        
        const int expected = 1;
        var actual = await repo.FindByTypeAndKey(TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        //Assert
        Assert.AreEqual(expected, actual.Count());
    }

    [Test]
    public async Task Can_Find_Tunes_By_Exact_Composer_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune1 = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        await repo.AddTune(tune1);
        var tune2 = Tune.CreateTune("Dram Circle", "Sean Heely", 
            TuneTypeEnum.Jig, TuneKeyEnum.DMaj);
        await repo.AddTune(tune2);
        
        //Act
        const int expected = 1;
        var actual = await repo.FindByExactComposer("Sean Heely");
        
        //Assert
        Assert.AreEqual(expected, actual.Count());

    }

    [Test]
    public async Task Can_Find_Tune_By_Exact_Title_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune1 = Tune.CreateTune("Kesh", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        await repo.AddTune(tune1);
        var tune2 = Tune.CreateTune("Rolling Waves", "Traditional", 
            TuneTypeEnum.Jig, TuneKeyEnum.DMaj);
        await repo.AddTune(tune2);

        //Act
        const int expected = 1;
        var actual = await repo.FindByExactTitle("Kesh");

        //Assert
        Assert.AreEqual(expected, actual.Count());
    }
}