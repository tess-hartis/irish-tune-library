using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            "Jig", "GMaj");
        var tune2 = Tune.CreateTune("Banshee", "Traditional",
            "Reel", "GMaj");
        
        //Act
        await repo.AddAsync(tune1);
        await repo.AddAsync(tune2);

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
           "Jig", "GMaj");
        await repo.AddAsync(tune);

        //Act
        await repo.DeleteAsync(tune.Id);
        var tunes = await repo.GetEntities().ToListAsync();
        const int expected = 0;
        var actual = tunes.Count;
        
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
            "Jig", "GMaj");
        await repo.AddAsync(tune1);
        var tune2 = Tune.CreateTune("Rolling Waves", "Traditional", 
            "Jig", "DMaj");
        await repo.AddAsync(tune2);

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
        var tunes = await repo.GetEntities().ToListAsync();
        const int expected = 0;
        var actual = tunes.Count;
       
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
            "Jig", "GMaj");
        await repo.AddAsync(tune);
        
        //Act
        await repo.AddAlternateTitle(tune.Id, "Extra Title");

        const bool expected = true;
        var actual = tune.AlternateTitles.Contains("Extra Title");
        
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
            "Jig", "GMaj");
        
        //Act
        await repo.AddAsync(tune);
        var expected = new DateOnly(2022, 01, 22);
        var actual = tune.DateAdded;
        
        //Assert
        Assert.AreEqual(expected, actual);
    }
    
}