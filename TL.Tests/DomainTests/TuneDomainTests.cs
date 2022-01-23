using System;
using NUnit.Framework;
using TL.Domain;

namespace TL.Tests.DomainTests;

[TestFixture]
public class TuneDomainTests
{
    [Test]
    public void CanCreateTune()
    {
        var tune = Tune.CreateTune("Title", "Composer", TuneTypeEnum.Air, TuneKeyEnum.Ador);
        
        Assert.AreEqual("Title", tune.Title);
        Assert.AreEqual("Composer", tune.Composer);
        Assert.AreEqual(TuneTypeEnum.Air, tune.TuneType);
        Assert.AreEqual(TuneKeyEnum.Ador, tune.TuneKey);
    }

    [Test]
    public void Cannot_Create_When_Title_Invalid()
    {
        Assert.Throws<FormatException>(() => 
            Tune.CreateTune("", "Composer", TuneTypeEnum.Air, TuneKeyEnum.Ador));
    }
    
    [Test]
    public void Cannot_Create_When_Composer_Invalid()
    {
        Assert.Throws<FormatException>(() => 
            Tune.CreateTune("Title", "", TuneTypeEnum.Air, TuneKeyEnum.Ador));
    }

    [Test]
    public void Can_Update_Properties_Using_Access_Methods()
    {
        //Arrange
        var tune = Tune.CreateTune("Title", "Composer", TuneTypeEnum.Air, TuneKeyEnum.Ador);
        
        //Act
        tune.UpdateTitle("New Title");
        tune.UpdateComposer("New Composer");
        tune.UpdateType(TuneTypeEnum.Fling);
        tune.UpdateKey(TuneKeyEnum.Amin);
        
        //Assert
        Assert.AreEqual("New Title", tune.Title);
        Assert.AreEqual("New Composer", tune.Composer);
        Assert.AreEqual(TuneTypeEnum.Fling, tune.TuneType);
        Assert.AreEqual(TuneKeyEnum.Amin, tune.TuneKey);
    }

    [Test]
    public void Cannot_Update_Title_With_Invalid_Input()
    {
        //Arrange
        var tune = Tune.CreateTune("Title", "Composer", TuneTypeEnum.Air, TuneKeyEnum.Ador);
        
        //Assert
        Assert.Throws<FormatException>(() => tune.UpdateTitle(""));
    }
    
    [Test]
    public void Cannot_Update_Composer_With_Invalid_Input()
    {
        //Arrange
        var tune = Tune.CreateTune("Title", "Composer", TuneTypeEnum.Air, TuneKeyEnum.Ador);
        
        //Assert
        Assert.Throws<FormatException>(() => tune.UpdateComposer(""));
    }
    
    
    
}