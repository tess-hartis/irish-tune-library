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
    
    public void Can_Add_Tune_Using_Context_Directly()
    {
        //Arrange
        using var context = new TuneLibraryContext(); // need to extract these three lines
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var tune = new Tune("Helvic Head", TuneTypeEnum.Jig,
            TuneKeyEnum.GMaj, "Traditional");

        //Act
        context.Tunes.Add(tune);
        context.SaveChanges();

        const int expected = 0;
        var actual = tune.Id;

        //Assert
        Assert.AreNotEqual(expected, actual);
    }

    [Test]
    public async Task Can_Add_Tune_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var repo = new TuneRepository(context);
        var tune1 = new Tune("Helvic Head", TuneTypeEnum.Jig,
            TuneKeyEnum.GMaj, "Traditional");
        var tune2 = new Tune("Bucks of Oranmore", TuneTypeEnum.Reel,
            TuneKeyEnum.GMaj, "Traditional");
        var tune3 = new Tune("Gravel Walks", TuneTypeEnum.Reel,
            TuneKeyEnum.GMaj, "Traditional");

        //Act
        await repo.Add(tune1);
        await repo.Add(tune2);
        await repo.Add(tune3);
        
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
        var tune = new Tune("Kesh", TuneTypeEnum.Jig,
            TuneKeyEnum.GMaj, "Traditional");
        await repo.Add(tune);

        //Act
        await repo.Delete(tune.Id);
        var tunes = await repo.GetAllTunes();
        const int expected = 0;
        var actual = tunes.Count();
        
        //Assert
        Assert.AreEqual(expected, actual);
        
    }

    [Test]
    public async Task Can_Update_Tune_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune1 = new Tune("Kesh", TuneTypeEnum.Jig, TuneKeyEnum.GMaj,
            "Traditional");
        await repo.Add(tune1);

        //Act
        tune1.Title = "New Title";
        await repo.Update(tune1.Id);

        const string expected = "New Title";
        var actual = tune1.Title;

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
        var tune1 = new Tune("Kesh", TuneTypeEnum.Jig, TuneKeyEnum.GMaj,
            "Traditional");
        await repo.Add(tune1);
        var tune2 = new Tune("Rolling Waves", TuneTypeEnum.Jig, TuneKeyEnum.GMaj,
            "Traditional");
        await repo.Add(tune2);

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
        var tune = new Tune("Donnybrook Fair", TuneTypeEnum.Jig,
            TuneKeyEnum.GMaj, "Traditional");
        await repo.Add(tune);
        
        //Act
        await repo.AddAlternateTitlesAsync(tune.Id, new List<string> {"extra title"});

        const string expected = "extra title";
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
        var tune = new Tune("Donnybrook Fair", TuneTypeEnum.Jig,
            TuneKeyEnum.GMaj, "Traditional");
        
        //Act
        await repo.Add(tune);
        var expected = new DateOnly(2022, 01, 12);
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
        var tune1 = new Tune("Donnybrook Fair", TuneTypeEnum.Jig,
            TuneKeyEnum.GMaj, "Traditional");
        await repo.Add(tune1);
        var tune2 = new Tune("Boys of Ballycastle", TuneTypeEnum.Reel,
            TuneKeyEnum.DMaj, "Traditional");
        await repo.Add(tune2);
        
        //Act
        
        const int expected = 1;
        var actual = await repo.SortByTuneTypeAsync(TuneTypeEnum.Jig);
        
        //Assert
        Assert.AreEqual(expected, actual.Count());
    }

    [Test]
    public async Task Can_Find_All_Tunes_By_TuneKey_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune1 = new Tune("Donnybrook Fair", TuneTypeEnum.Jig,
            TuneKeyEnum.GMaj, "Traditional");
        await repo.Add(tune1);
        
        var tune2 = new Tune("Boys of Ballycastle", TuneTypeEnum.Reel,
            TuneKeyEnum.DMaj, "Traditional");
        await repo.Add(tune2);
        
        //Act
        const int expected = 1;
        var actual = await repo.SortByTuneKeyAsync(TuneKeyEnum.DMaj);
        
        //Assert
        Assert.AreEqual(expected, actual.Count());
    }

    [Test]
    public async Task Can_Find_All_Tunes_By_TuneType_And_TuneKey_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune1 = new Tune("Donnybrook Fair", TuneTypeEnum.Jig,
            TuneKeyEnum.GMaj, "Traditional");
        await repo.Add(tune1);
        var tune2 = new Tune("Boys of Ballycastle", TuneTypeEnum.Reel,
            TuneKeyEnum.DMaj, "Traditional");
        await repo.Add(tune2);

        //Act
        
        const int expected = 1;
        var actual = await repo.SortByTypeAndKeyAsync(TuneTypeEnum.Jig, TuneKeyEnum.GMaj);
        //Assert
        Assert.AreEqual(expected, actual.Count());
    }

    [Test]
    public async Task Can_Find_All_Tunes_By_Exact_Composer_Using_Repository()
    {
        //Arrange
        await using var context = new TuneLibraryContext();
        var repo = new TuneRepository(context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var tune1 = new Tune("Donnybrook Fair", TuneTypeEnum.Jig,
            TuneKeyEnum.GMaj, "Traditional");
        await repo.Add(tune1);
        var tune2 = new Tune("Dram Circle", TuneTypeEnum.Jig,
            TuneKeyEnum.DMaj, "Sean Heely");
        await repo.Add(tune2);
        
        //Act
        const int expected = 1;
        var actual = await repo.SortByExactComposerAsync("Sean Heely");
        
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
        var tune1 = new Tune("Kesh", TuneTypeEnum.Jig, TuneKeyEnum.GMaj,
            "Traditional");
        await repo.Add(tune1);
        var tune2 = new Tune("Rolling Waves", TuneTypeEnum.Jig, TuneKeyEnum.GMaj,
            "Traditional");
        await repo.Add(tune2);

        //Act
        const int expected = 1;
        var actual = await repo.GetByTitle("Kesh");

        //Assert
        Assert.AreEqual(expected, actual.Count());
    }
}